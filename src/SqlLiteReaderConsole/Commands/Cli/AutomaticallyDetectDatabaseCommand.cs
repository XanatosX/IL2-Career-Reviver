using IL2CarrerReviverConsole.Services;
using IL2CarrerReviverModel.Services.SaveGame;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace IL2CarrerReviverConsole.Commands.Cli;

[Description("Automaticly detect the game folder")]
internal class AutomaticallyDetectDatabaseCommand : Command
{
    private readonly ISettingsService settingsService;
    private readonly IEnumerable<ISavegameLocatorService> savegameLocatorServices;

    public AutomaticallyDetectDatabaseCommand(ISettingsService settingsService, IEnumerable<ISavegameLocatorService> savegameLocatorServices)
    {
        this.settingsService = settingsService;
        this.savegameLocatorServices = savegameLocatorServices;
    }

    public override int Execute(CommandContext context)
    {
        if (!AnsiConsole.Confirm("Do you really want to allow the program to scan your computer for the databse?"))
        {
            AnsiConsole.MarkupLine("[Green]Scan was aborted by user[/]");
            return 1;
        }
        foreach (var service in savegameLocatorServices.OrderBy(service => service.Priority))
        {
            string? path = null;
            AnsiConsole.Status().Start($"Running service {service.DisplayName}", ctx =>
            {
                path = service.GetSavegamePath();
            });

            if (!string.IsNullOrWhiteSpace(path))
            {
                if (AnsiConsole.Confirm($"Found path [bold green]{path}[/] do you want to use this as your gamepath?"))
                {
                    settingsService.UpdateSettings(setting =>
                    {
                        setting.DatabasePath = path;
                    });
                    if (settingsService?.GetSettings()?.DatabasePath == path)
                    {
                        AnsiConsole.MarkupLine("[green]Path was saved successfully[/]");
                        return 0;
                    }
                    AnsiConsole.MarkupLine("[red]Path was not saved![/]");
                    return 0;
                }
            }
        }
        AnsiConsole.MarkupLine("[red]Could not find path with available automatic detection services, please use the manuell approach[/]");
        return 1;
    }
}
