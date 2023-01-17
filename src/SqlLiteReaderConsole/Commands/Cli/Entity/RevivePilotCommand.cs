using IL2CarrerReviverConsole.Commands.Cli.Settings;
using IL2CarrerReviverConsole.Services;
using IL2CarrerReviverModel.Data.Gateways;
using IL2CarrerReviverModel.Models;
using IL2CarrerReviverModel.Services;
using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL2CarrerReviverConsole.Commands.Cli.Entity;
internal class RevivePilotCommand : Command<RevivePilotCommandSettings>
{
    private readonly IPilotGateway pilotGateway;
    private readonly ICareerGateway careerGateway;
    private readonly IMissionGateway missionGateway;
    private readonly ISortieGateway sortieGateway;
    private readonly ResourceFileReader resourceFileReader;
    private readonly IByteArrayToDateTimeService byteArrayToDateTime;

    public RevivePilotCommand(IPilotGateway pilotGateway,
                              ICareerGateway careerGateway,
                              IMissionGateway missionGateway,
                              ISortieGateway sortieGateway,
                              ResourceFileReader resourceFileReader,
                              IByteArrayToDateTimeService byteArrayToDateTime)
    {
        this.pilotGateway = pilotGateway;
        this.careerGateway = careerGateway;
        this.missionGateway = missionGateway;
        this.sortieGateway = sortieGateway;
        this.resourceFileReader = resourceFileReader;
        this.byteArrayToDateTime = byteArrayToDateTime;
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] RevivePilotCommandSettings settings)
    {
        string warning = resourceFileReader.GetResourceContent("ReviveWarning.txt");
        AnsiConsole.Markup($"{warning}{Environment.NewLine}");
        if (!AnsiConsole.Confirm("Do you accept this warning?"))
        {
            return 0;
        }
        var possibleReviceCandidates = careerGateway.GetAll()
                                            .Where(career => career.Player.State != 0)
                                            .Select(career => career.Player)
                                            .ToList();

        if (!possibleReviceCandidates.Any())
        {
            AnsiConsole.MarkupLine("[green]Could not find any pilot which can be revived[/]");
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
        career.State = 0;


        var sortiesDiedIn = sortieGateway.GetAll(sortie => sortie.PilotId == selectedPilot.Id)
                                         .Where(sortie => sortie.Status == 2);
        var sortieDataToDelete = sortieGateway.GetAll(sortie => sortiesDiedIn.Select(sortie => sortie.MissionId).Contains(sortie.MissionId))
                                              .ToList();



        long highestNumer = sortieDataToDelete.Select(sortie => sortie.MissionId).Max();

        var missionsToDelete = missionGateway.GetAll(mission => mission.Id >= highestNumer && mission.SquadronId == career.Squadron.Id)
                                             .ToList();


        AnsiConsole.MarkupLine("All data collected, starting to revive");
        careerGateway.UpdateCareer(career);
        bool careerUpdateSuccessful = careerGateway.GetById((int)career.Id)?.State == 0;
        GetColorForStatus(careerUpdateSuccessful);
        AnsiConsole.MarkupLine($"Career update status: {GetColorForStatus(careerUpdateSuccessful)}{careerUpdateSuccessful}[/]");

        List<bool> sortieDeleteStatus = new List<bool>();
        foreach (var sortie in sortieDataToDelete)
        {
            sortieDeleteStatus.Add(sortieGateway.DeleteById(sortie.Id));
        }

        int successfulDelete = sortieDeleteStatus.Where(deleteStatus => deleteStatus).Count();
        int totalDelete = sortieDataToDelete.Count;
        bool sortieDeleteOverallStatus = successfulDelete == totalDelete;
        AnsiConsole.MarkupLine($"Sortie deletion status: {GetColorForStatus(sortieDeleteOverallStatus)}{sortieDeleteOverallStatus}[/]");
        AnsiConsole.MarkupLine($"Deleted: {successfulDelete} / {totalDelete}");

        return 0;
    }

    private string GetColorForStatus(bool status)
    {
        return status ? "[green]" : "[red]";
    }
}
