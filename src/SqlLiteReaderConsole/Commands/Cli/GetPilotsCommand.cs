using IL2CarrerReviverConsole.Commands.Cli.Settings;
using IL2CarrerReviverConsole.Views;
using IL2CarrerReviverModel.Data.Gateways;
using IL2CarrerReviverModel.Models;
using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL2CarrerReviverConsole.Commands.Cli;
internal class GetPilotsCommand : Command<GetPilotSettings>
{
    private readonly IPilotGateway pilotGateway;
    private readonly ViewFactory viewFactory;

    public GetPilotsCommand(IPilotGateway pilotGateway, ViewFactory viewFactory)
    {
        this.pilotGateway = pilotGateway;
        this.viewFactory = viewFactory;
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] GetPilotSettings settings)
    {
        var pilots = settings.Name is null ? pilotGateway.GetAll() : pilotGateway.GetAll(pilot => pilot.Name == settings.Name || pilot.LastName == settings.Name);
        List<Pilot> pilotsToRender = pilots?.ToList() ?? new List<Pilot>();
        if (pilotsToRender.Count == 1)
        {
            IView<Pilot>? view = viewFactory.CreateView<PilotView>();
            AnsiConsole.Write(view?.GetView(pilotsToRender.First()) ?? new Markup("[red]Could not find view to display single pilot[/]"));
            return 0;
        }
        IView<IEnumerable<Pilot>>? tableView = viewFactory.CreateView<PilotTableView>();
        AnsiConsole.Write(tableView?.GetView(pilotsToRender) ?? new Markup("[red]Could not find view to display multiple pilot[/]"));
        return 0;
    }
}
