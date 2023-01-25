using IL2CarrerReviverConsole.Commands.Cli.Settings;
using IL2CarrerReviverConsole.Services;
using IL2CarrerReviverModel.Data.Gateways;
using IL2CarrerReviverModel.Models;
using IL2CarrerReviverModel.Services;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace IL2CarrerReviverConsole.Commands.Cli.Entity;

[Description("Revive a pilot from a career")]
internal class RevivePilotCommand : Command<RevivePilotCommandSettings>
{
    private readonly IPilotGateway pilotGateway;
    private readonly ICareerGateway careerGateway;
    private readonly IMissionGateway missionGateway;
    private readonly ISortieGateway sortieGateway;
    private readonly ResourceFileReader resourceFileReader;
    private readonly IDatabaseBackupService databaseBackupService;
    private readonly IByteArrayToDateTimeService byteArrayToDateTime;

    public RevivePilotCommand(IPilotGateway pilotGateway,
                              ICareerGateway careerGateway,
                              IMissionGateway missionGateway,
                              ISortieGateway sortieGateway,
                              ResourceFileReader resourceFileReader,
                              IDatabaseBackupService databaseBackupService,
                              IByteArrayToDateTimeService byteArrayToDateTime)
    {
        this.pilotGateway = pilotGateway;
        this.careerGateway = careerGateway;
        this.missionGateway = missionGateway;
        this.sortieGateway = sortieGateway;
        this.resourceFileReader = resourceFileReader;
        this.databaseBackupService = databaseBackupService;
        this.byteArrayToDateTime = byteArrayToDateTime;
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] RevivePilotCommandSettings settings)
    {
        var possibleReviceCandidates = careerGateway.GetAll()
                                            .Where(career => settings.IncludeIronMan || career.IronMan == 0)
                                            .Where(career => career?.Player?.State != 0)
                                            .Select(career => career.Player)
                                            .OfType<Pilot>()
                                            .ToList();

        if (!possibleReviceCandidates.Any())
        {
            AnsiConsole.MarkupLine("[green]Could not find any pilot which can be revived[/]");
            return 0;
        }

        string warning = resourceFileReader.GetResourceContent("ReviveWarning.txt");
        AnsiConsole.Markup($"{warning}{Environment.NewLine}");
        if (!AnsiConsole.Confirm("Do you accept this warning?"))
        {
            return 0;
        }

        var selectedPilot = AnsiConsole.Prompt(new SelectionPrompt<Pilot>().Title("Select pilot for reviving (Name Lastname - Airfield (Creation Date))")
                                                                           .UseConverter(pilot => $"{pilot.Name} {pilot.LastName} - {pilot.Squadron.Airfield} ({byteArrayToDateTime.GetDateTime(pilot.InsDate ?? Array.Empty<byte>())})")
                                                                           .AddChoices(possibleReviceCandidates));
        if (selectedPilot is null)
        {
            AnsiConsole.MarkupLine("No pilot selected");
            return 1;
        }

        if (!AnsiConsole.Confirm($"Do you really want to revive {selectedPilot.Name} {selectedPilot.LastName}"))
        {
            return 1;
        }

        var career = careerGateway.GetAll().Where(career => career.Player.Id == selectedPilot.Id).FirstOrDefault();
        if (career is null)
        {
            AnsiConsole.MarkupLine("Could not get career for pilot");
            return 1;
        }


        var sortiesDiedIn = sortieGateway.GetAll(sortie => sortie.PilotId == selectedPilot.Id)
                                         .Where(sortie => sortie.Status == 2).ToList();
        var sortieDataToDelete = sortieGateway.GetAll(sortie => sortiesDiedIn.Select(sortie => sortie.MissionId).Contains(sortie.MissionId))
                                              .ToList();

        long highestNumer = long.MaxValue;
        if (sortieDataToDelete.Count > 0)
        {
            highestNumer = sortieDataToDelete.Select(sortie => sortie.MissionId).Max();
        }

        var missionsToDelete = missionGateway.GetAll(mission => mission.Id >= highestNumer && mission.SquadronId == career.Squadron.Id)
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

        List<TableColumn> columns = new List<TableColumn>();

        AnsiConsole.Status().Start("All data collected, starting to revive", ctx =>
        {
            Thread.Sleep(200);
            ctx.Status("Resetting career");
            var missionIdToGet = missionsToDelete.Count > 0 ? missionsToDelete.Select(mission => mission.Id).Min() : -1;
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
                                        $"Reseting status and date for career with id [yellow]{career.Id}[/]",
                                        "Career",
                                        "Changed",
                                        careerUpdateSuccessful));

            ctx.Status("Resetting Player");
            career.Player.State = 0;
            pilotUpdateSuccessful = pilotGateway.Update(career.Player)?.State == 0;
            columns.Add(new TableColumn(career.Id.ToString(),
                            $"Reseting status for player with id [yellow]{career.Player.Id}[/]",
                            "Player",
                            "Changed",
                            pilotUpdateSuccessful));

            ctx.Status("Resetting Sorties");
            List<bool> sortieDeleteStatus = new List<bool>();
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
            List<bool> missionDeleteStatus = new List<bool>();
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
            successfulMissionDelete = missionDeleteStatus.Where(deleteStatus => deleteStatus).Count();
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

        Table overviewTable = new Table();
        overviewTable.AddColumns("Id", "Name", "Type", "Action", "Successful");
        foreach (var column in columns)
        {
            overviewTable.AddRow(column.id, column.name, column.type, column.action, $"{GetColorForStatus(column.status)}{column.status}[/]");
        }
        AnsiConsole.MarkupLine("[yellow]Summary of changes[/]");
        AnsiConsole.Write(overviewTable);

        return 0;
    }

    private string GetColorForStatus(bool status)
    {
        return status ? "[green on grey27]" : "[red on grey27]";
    }

    record TableColumn(string id, string name, string type, string action, bool status);
}
