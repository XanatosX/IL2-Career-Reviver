using IL2CarrerReviverConsole.Commands.Cli.Settings;
using IL2CarrerReviverConsole.Services;
using IL2CarrerReviverConsole.Views;
using IL2CarrerReviverModel.Data.Gateways;
using IL2CarrerReviverModel.Models;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Diagnostics.CodeAnalysis;

namespace IL2CarrerReviverConsole.Commands.Cli;
internal class GetPilotsCommand : Command<GetPilotSettings>
{
    private readonly IPilotGateway pilotGateway;
    private readonly ViewFactory viewFactory;
    private readonly ILogger<GetPilotsCommand> logger;

    public GetPilotsCommand(IPilotGateway pilotGateway, ViewFactory viewFactory, ILogger<GetPilotsCommand> logger)
    {
        this.pilotGateway = pilotGateway;
        this.viewFactory = viewFactory;
        this.logger = logger;
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] GetPilotSettings settings)
    {
        logger.LogInformation("Get pilots");
        var pilots = settings.Name is null ? pilotGateway.GetAll() : pilotGateway.GetAll(pilot => pilot.Name == settings.Name || pilot.LastName == settings.Name);
        List<Pilot> pilotsToRender = pilots?.ToList() ?? new List<Pilot>();
        if (pilotsToRender.Count == 1)
        {
            logger.LogDebug($"Found a single pilot to render");
            IView<Pilot>? view = viewFactory.CreateView<PilotView>();
            AnsiConsole.Write(view?.GetView(pilotsToRender.First()) ?? new Markup("[red]Could not find view to display single pilot[/]"));
            return 0;
        }
        logger.LogDebug($"Found a number of {pilotsToRender.Count} pilots to render");
        IView<IEnumerable<Pilot>>? tableView = viewFactory.CreateView<PilotTableView>();
        AnsiConsole.Write(tableView?.GetView(pilotsToRender) ?? new Markup("[red]Could not find view to display multiple pilot[/]"));
        return 0;
    }
}
