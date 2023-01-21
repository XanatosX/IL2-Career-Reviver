using IL2CarrerReviverConsole.Commands.Cli.Settings;
using IL2CarrerReviverConsole.Services;
using IL2CarrerReviverModel.Models;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace IL2CarrerReviverConsole.Commands.Cli.Save;

[Description("Change the name for a backup")]
internal class ChangeBackupNameCommand : Command<ChangeBackupNameSettings>
{
    private readonly IDatabaseBackupService databaseBackupService;
    private readonly ILogger<ChangeBackupNameCommand> logger;

    public ChangeBackupNameCommand(IDatabaseBackupService databaseBackupService, ILogger<ChangeBackupNameCommand> logger)
    {
        this.databaseBackupService = databaseBackupService;
        this.logger = logger;
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] ChangeBackupNameSettings settings)
    {
        var backup = databaseBackupService.GetBackups().Where(b => b.Guid == settings.Guid).FirstOrDefault();
        if (backup is not null && !string.IsNullOrEmpty(settings.Name))
        {
            return UpdateNameForBackup(backup, settings.Name);
        }
        if (backup is null && !string.IsNullOrEmpty(settings.Name))
        {
            logger.LogWarning($"Could not find any backup with guid {settings.Guid}");
            AnsiConsole.MarkupLine($"[red]Could not find any backup with guid {settings.Guid}, use the list command to get valids guid's[/]");
            return 1;
        }
        var selection = AnsiConsole.Prompt(new SelectionPrompt<DatabaseBackup>().Title("Select backup to rename")
                                                                                .UseConverter(b => b.DisplayName)
                                                                                .AddChoices(databaseBackupService.GetBackups()));
        if (selection is null)
        {
            AnsiConsole.MarkupLine("[red]Nothing was selected![/]");
            return 1;
        }

        string newName = AnsiConsole.Ask<string>($"Please enter new backup name for {selection.DisplayName}:");
        if (AnsiConsole.Confirm($"Do you really want to rename [yellow]{selection.DisplayName}[/] to [yellow]{newName}[/]?"))
        {
            return UpdateNameForBackup(selection, newName);
        }
        return 0;
    }

    private int UpdateNameForBackup(DatabaseBackup backup, string newName)
    {
        string oldName = backup.DisplayName;
        if (databaseBackupService.UpdateBackupName(backup, newName))
        {
            logger.LogDebug($"Renaming backup {oldName} to {newName} was successful");
            AnsiConsole.MarkupLine($"[green]Renaming backup {oldName} to {newName} was successful[/]");
            return 0;
        }
        AnsiConsole.MarkupLine($"[red]Renaming backup {oldName} to {newName} failed, please check the log for more information[/]");
        return 1;
    }
}
