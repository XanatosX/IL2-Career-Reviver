using IL2CareerModel.Services;
using IL2CareerToolset.Commands.Cli.Settings;
using IL2CareerToolset.Services;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
namespace IL2CareerToolset.Commands.Cli;

[Description("Command to set the game path by yourself")]
internal class ManuelDatabaseCommand : Command<ManuelDatabaseCommandSettings>
{
    private readonly IGamePathValidationService pathValidationService;
    private readonly ISettingsService settingsService;
    private readonly ILogger<ManuelDatabaseCommand> logger;

    public ManuelDatabaseCommand(IGamePathValidationService pathValidationService, ISettingsService settingsService, ILogger<ManuelDatabaseCommand> logger)
    {
        this.pathValidationService = pathValidationService;
        this.settingsService = settingsService;
        this.logger = logger;
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] ManuelDatabaseCommandSettings settings)
    {
        return settings.IsInteractiv ? GetPathInteractive() : UseSettingPath(settings.GameRootFolder!);
    }

    private int UseSettingPath(string gameRootFolder)
    {
        if (pathValidationService.IsValidGamePath(gameRootFolder))
        {
            SaveValidPath(gameRootFolder);
            return 0;
        }
        AnsiConsole.MarkupLine($"[red]The provided path \"{gameRootFolder}\" is not valid![/]");
        return 1;
    }

    private int GetPathInteractive()
    {
        AnsiConsole.MarkupLine("[red]No Path provided as argument[/]");
        AnsiConsole.MarkupLine("[yellow]Press ctrl + c to abort the dialog[/]");
        bool isValidPath = false;
        string rootFolder = string.Empty;
        while (!isValidPath)
        {
            rootFolder = AnsiConsole.Ask<string>("Please enter the root path of the game folder:");
            isValidPath = pathValidationService.IsValidGamePath(rootFolder);
            if (!isValidPath)
            {
                AnsiConsole.MarkupLine($"[red]The provided path: {rootFolder} is not valid![/]");
            }
        }
        SaveValidPath(rootFolder);

        return 0;
    }

    private void SaveValidPath(string rootFolder)
    {
        AnsiConsole.Clear();
        string fullPath = pathValidationService.GetPathToDatabase(rootFolder) ?? string.Empty;
        AnsiConsole.MarkupLine("[green]The path is valid[/]");
        AnsiConsole.Write(new TextPath(fullPath));
        if (AnsiConsole.Confirm("Do you want to save the path now?"))
        {
        settingsService.UpdateSettings(setting =>
        {
            setting.DatabasePath = fullPath;
        });
                if (settingsService?.GetSettings()?.DatabasePath == fullPath)
                {
                    AnsiConsole.MarkupLine("[green]Path was saved successfully[/]");
                    return;
                }
                AnsiConsole.MarkupLine("[red]Path was not saved![/]");
        }
        
    }
}
