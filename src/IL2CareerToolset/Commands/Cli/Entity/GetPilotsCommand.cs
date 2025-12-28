using IL2CareerModel.Data.Gateways;
using IL2CareerModel.Models;
using IL2CareerToolset.Services;
using IL2CareerToolset.Views;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace IL2CareerToolset.Commands.Cli.Entity;

internal class GetPilotSettings : CommandSettings
{
    [CommandArgument(0, "[Pilot Name]")]
    [Description("The name of the pilot to search")]
    public string? Name { get; set; }

    [CommandOption("-p|--player")]
    [Description("Show player only pilots")]
    public bool PlayerOnly { get; set; }
}

[Description("Get all pilots listed in a table")]
internal class GetPilotsCommand : Command<GetPilotSettings>
{
    private readonly IPilotGateway pilotGateway;
    private readonly ICareerGateway careerGateway;
    private readonly ViewFactory viewFactory;
    private readonly ISettingsService settingsService;
    private readonly ILogger<GetPilotsCommand> logger;

    public GetPilotsCommand(IPilotGateway pilotGateway,
                            ICareerGateway careerGateway,
                            ViewFactory viewFactory,
                            ISettingsService settingsService,
                            ILogger<GetPilotsCommand> logger)
    {
        this.pilotGateway = pilotGateway;
        this.careerGateway = careerGateway;
        this.viewFactory = viewFactory;
        this.settingsService = settingsService;
        this.logger = logger;
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] GetPilotSettings settings)
    {
        logger.LogInformation("Get pilots");
        var setting = settingsService.GetSettings();
        if (setting is null || setting.DatabasePath is null || !File.Exists(setting.DatabasePath))
        {
            AnsiConsole.MarkupLine("[red]Either database is not set, was moved or deleted! Please set again with the 'settings' command[/]");
            return 1;
        }
        var pilots = settings.PlayerOnly ? careerGateway.GetAll()
                                                        .Select(career => career.Player)
                                                        .OfType<Pilot>() : pilotGateway.GetAll(pilot => PilotFilter(pilot, settings.Name));

        List<Pilot> pilotsToRender = pilots?.ToList() ?? new();
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

    private bool PilotFilter(Pilot pilot, string? name)
    {
        return name is null || pilot.Name == name || pilot.LastName == name;
    }
}
