using IL2CareerModel.Data.Gateways;
using IL2CareerModel.Models;
using IL2CareerModel.Models.Database;
using IL2CareerModel.Services;
using IL2CareerToolset.Services;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace IL2CareerToolset.Commands.Cli.Entity;

[Description("Command to revive a pilot of the campaign")]
internal class RevivePilotCommandSettings : CommandSettings
{
    [CommandOption("-i|--ironman")]
    [Description("Include iron man characters")]
    public bool IncludeIronMan { get; set; }
}

[Description("Revive a pilot from a career")]
internal class RevivePilotCommand : Command<RevivePilotCommandSettings>
{
    private readonly IPilotGateway pilotGateway;
    private readonly ICareerGateway careerGateway;
    private readonly IMissionGateway missionGateway;
    private readonly ISortieGateway sortieGateway;
    private readonly ISettingsService settingsService;
    private readonly IPilotStateService pilotStateService;
    private readonly IDatabaseBackupService backupService;
    private readonly ResourceFileReader resourceFileReader;
    private readonly IByteArrayToDateTimeService byteArrayToDateTime;

    public RevivePilotCommand(IPilotGateway pilotGateway,
                              ICareerGateway careerGateway,
                              IMissionGateway missionGateway,
                              ISortieGateway sortieGateway,
                              ISettingsService settingsService,
                              IPilotStateService pilotStateService,
                              IDatabaseBackupService backupService,
                              ResourceFileReader resourceFileReader,
                              IByteArrayToDateTimeService byteArrayToDateTime)
    {
        this.pilotGateway = pilotGateway;
        this.careerGateway = careerGateway;
        this.missionGateway = missionGateway;
        this.sortieGateway = sortieGateway;
        this.settingsService = settingsService;
        this.pilotStateService = pilotStateService;
        this.backupService = backupService;
        this.resourceFileReader = resourceFileReader;
        this.byteArrayToDateTime = byteArrayToDateTime;
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] RevivePilotCommandSettings settings)
    {
        var setting = settingsService.GetSettings();
        if (setting is null || setting.DatabasePath is null || !File.Exists(setting.DatabasePath))
        {
            AnsiConsole.MarkupLine("[red]Either database is not set, was moved or deleted! Please set again with the 'settings' command[/]");
            return 1;
        }
        AnsiConsole.MarkupLine("[yellow]Create Database backup[/]");
        var createdBackup = backupService.CreateBackup($"Automatically created before revive attempt at {DateTime.Now}");
        if (createdBackup is not null)
        {
            AnsiConsole.MarkupLine($"[green]Backup \"{createdBackup.DisplayName}\" with guid \"{createdBackup.Guid}\" was created[/]");
        }
        var possibleReviveCandidates = careerGateway.GetAll()
                                                    .OfType<Career>()
                                                    .Where(career => settings.IncludeIronMan || career.IronMan == 0)
                                                    .Where(career => career?.Player is not null)
                                                    .Where(career => !pilotStateService.PilotIsAlive(career!.Player!))
                                                    .Select(career => career.Player)
                                                    .OfType<Pilot>()
                                                    .ToList();

        if (!possibleReviveCandidates.Any())
        {
            AnsiConsole.MarkupLine("[green]Could not find any pilot which can be revived[/]");
            DeleteBackup(createdBackup);
            return 0;
        }

        string warning = resourceFileReader.GetResourceContent("ReviveWarning.txt");
        AnsiConsole.Markup($"{warning}{Environment.NewLine}");
        if (!AnsiConsole.Confirm("Do you accept this warning?"))
        {
            DeleteBackup(createdBackup);
            return 0;
        }

        var selectedPilot = AnsiConsole.Prompt(new SelectionPrompt<Pilot>().Title("Select pilot for reviving (Name Lastname - Airfield (Creation Date))")
                                                                           .UseConverter(pilot => $"{pilot.Name} {pilot.LastName} - {pilot.Squadron?.Airfield ?? string.Empty} ({byteArrayToDateTime.GetDateTime(pilot.InsDate ?? Array.Empty<byte>())})")
                                                                           .AddChoices(possibleReviveCandidates));
        if (selectedPilot is null)
        {
            AnsiConsole.MarkupLine("No pilot selected");
            DeleteBackup(createdBackup);
            return 1;
        }

        if (!AnsiConsole.Confirm($"Do you really want to revive {selectedPilot.Name} {selectedPilot.LastName}"))
        {
            DeleteBackup(createdBackup);
            return 1;
        }

        var career = careerGateway.GetAll().Where(career => career.Player?.Id == selectedPilot.Id).FirstOrDefault();
        if (career is null || career.Squadron is null || career.Player is null)
        {
            AnsiConsole.MarkupLine("Could not get career for pilot");
            return 1;
        }

        
        

        var sortiesDiedIn = sortieGateway.GetAll(sortie => sortie.PilotId == selectedPilot.Id)
                                         .Where(sortie => sortie.Status == 2).ToList();
        var sortieDataToDelete = sortieGateway.GetAll(sortie => sortiesDiedIn.Select(deadSortie => deadSortie.MissionId).Contains(sortie.MissionId) && sortie.PilotId == selectedPilot.Id)
                                              .ToList();

        long highestNumber = long.MaxValue;
        if (sortieDataToDelete.Count > 0)
        {
            highestNumber = sortieDataToDelete.Select(sortie => sortie.MissionId).Max();
        }

        var missionsToDelete = missionGateway.GetAll(mission => mission.Id >= highestNumber && MissionContainsPilot(selectedPilot, mission))
                                             .ToList();

        bool careerUpdateSuccessful = false;
        bool pilotUpdateSuccessful = false;
        bool sortieDeleteOverallStatus = false;
        bool missionDeleteOverallStatus = false;
        bool overall = false;
        int successfulSortieDelete = 0;
        int totalSortieDelete = 0;
        int successfulMissionDelete = 0;
        int totalMissionDelete = 0;

        List<TableColumn> columns = new();

        AnsiConsole.Status().Start("All data collected, starting to revive", ctx =>
        {
            Thread.Sleep(200);
            ctx.Status("Resetting career");
            var missionIdToGet = missionsToDelete.Count > 0 ? missionsToDelete.Min(mission => mission.Id) : -1;
            var latestMission = missionGateway.GetAll(mission => mission.Id < missionIdToGet && mission.SquadronId == career.Squadron.Id)
                                              .OrderBy(mission => mission.Id)
                                              .LastOrDefault();
            if (missionIdToGet == -1 || latestMission is null)
            {
                AnsiConsole.MarkupLine("[red]Could not get latest mission where pilot was alive, aborting[/]");
                return;
            }

            career.CurrentDate = latestMission.Date;
            career.State = 0;
            careerUpdateSuccessful = careerGateway.Update(career)?.State == 0;
            columns.Add(new TableColumn(career.Id.ToString(),
                                        $"Resetting status and date for career with id [yellow]{career.Id}[/]",
                                        "Career",
                                        "Changed",
                                        careerUpdateSuccessful));

            ctx.Status("Resetting Player");
            career.Player.State = 0;
            pilotUpdateSuccessful = pilotGateway.Update(career.Player)?.State == 0;
            columns.Add(new TableColumn(career.Id.ToString(),
                            $"Resetting status for player with id [yellow]{career.Player.Id}[/]",
                            "Player",
                            "Changed",
                            pilotUpdateSuccessful));

            ctx.Status("Resetting Sorties");
            List<bool> sortieDeleteStatus = new();
            foreach (var sortie in sortieDataToDelete)
            {
                sortieDeleteStatus.Add(sortieGateway.DeleteById(sortie.Id));
            }
            for (int i = 0; i < sortieDataToDelete.Count; i++)
            {
                var sortie = sortieDataToDelete[i];
                bool status = sortieDeleteStatus[i];
                columns.Add(new TableColumn(
                    sortie.Id.ToString(),
                    $"Pilot statistic for pilot [yellow]{sortie.Name}[/] on mission id [yellow]{sortie.MissionId}[/] deleted",
                    "Sortie",
                    "Deleted",
                    status));
            }
            successfulSortieDelete = sortieDeleteStatus.Where(deleteStatus => deleteStatus).Count();

            totalSortieDelete = sortieDataToDelete.Count;
            sortieDeleteOverallStatus = successfulSortieDelete == totalSortieDelete;

            ctx.Status("Resetting Missions");
            List<bool> missionDeleteStatus = new();
            foreach (var mission in missionsToDelete)
            {
                missionDeleteStatus.Add(missionGateway.Delete(mission));
            }
            for (int i = 0; i < missionsToDelete.Count; i++)
            {
                var mission = missionsToDelete[i];
                bool status = missionDeleteStatus[i];
                columns.Add(new TableColumn(
                    mission.Id.ToString(),
                    $"Mission data for mission with id [yellow]{mission.Id}[/] with type [yellow]{mission.Type}[/] deleted",
                    "Mission",
                    "Deleted",
                    status));
            }
            successfulMissionDelete = missionDeleteStatus.Count(deleteStatus => deleteStatus);
            totalMissionDelete = missionsToDelete.Count;
            missionDeleteOverallStatus = successfulMissionDelete == totalMissionDelete;
            overall = missionDeleteOverallStatus && sortieDeleteOverallStatus && careerUpdateSuccessful && pilotUpdateSuccessful;
        });

        AnsiConsole.MarkupLine($"Career update status: {GetColorForStatus(careerUpdateSuccessful)}{careerUpdateSuccessful}[/]");
        AnsiConsole.MarkupLine($"Pilot update status: {GetColorForStatus(pilotUpdateSuccessful)}{pilotUpdateSuccessful}[/]");
        AnsiConsole.MarkupLine($"Sortie deletion status: {GetColorForStatus(sortieDeleteOverallStatus)}{sortieDeleteOverallStatus}[/]");
        AnsiConsole.MarkupLine($"Mission deletion status: {GetColorForStatus(missionDeleteOverallStatus)}{missionDeleteOverallStatus}[/]");
        AnsiConsole.MarkupLine($"Deleted: {successfulSortieDelete} / {totalSortieDelete} sorties");
        AnsiConsole.MarkupLine($"Deleted: {successfulMissionDelete} / {totalMissionDelete} missions");
        AnsiConsole.MarkupLine($"OverallStatus: {GetColorForStatus(overall)}{overall}[/]");

        Table overviewTable = new();
        overviewTable.AddColumns("Id", "Name", "Type", "Action", "Successful");
        foreach (var column in columns)
        {
            overviewTable.AddRow(column.Id, column.Name, column.Type, column.Action, $"{GetColorForStatus(column.Status)}{column.Status}[/]");
        }
        AnsiConsole.MarkupLine("[yellow]Summary of changes[/]");
        AnsiConsole.Write(overviewTable);

        if (!overall)
        {
            AnsiConsole.MarkupLine($"[red]Something went wrong during the revive process, please check the log and restore from the created backup \"{createdBackup?.Guid}\"![/]");
        }

        return 0;
    }

    private string GetColorForStatus(bool status)
    {
        return status ? "[green on grey27]" : "[red on grey27]";
    }

    record TableColumn(string Id, string Name, string Type, string Action, bool Status);

    private bool MissionContainsPilot(Pilot pilot, Mission mission)
    {
        if (pilot is null || mission is null)
        {
            return false;
        }
        var id = pilot.Id;
        if (id < 0)
        {
            return false;
        }

        return mission.Pilot0 == id
                || mission.Pilot1 == id
                || mission.Pilot2 == id
                || mission.Pilot3 == id
                || mission.Pilot4 == id
                || mission.Pilot5 == id
                || mission.Pilot6 == id
                || mission.Pilot7 == id
                || mission.Pilot8 == id;
    }

    /**
    * Deletes the provided backup from the system
    * @param backup The backup to delete
    */
    private void DeleteBackup(DatabaseBackup? backup)
    {
        if (backup == null)
        {
            return;
        }
        if (backupService.DeleteBackup(backup))
        {
            AnsiConsole.MarkupLine("[yellow]Deleted the automatically created backup as no changes were made[/]");
        }
    }
}
