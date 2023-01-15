using IL2CarrerReviverConsole.Commands.Cli.Settings;
using IL2CarrerReviverConsole.Services;
using IL2CarrerReviverModel.Models;
using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace IL2CarrerReviverConsole.Commands.Cli;
internal class DeleteBackupsCommand : Command<DeleteBackupsCommandSettings>
{
    private readonly IDatabaseBackupService databaseBackupService;

    public DeleteBackupsCommand(IDatabaseBackupService databaseBackupService)
    {
        this.databaseBackupService = databaseBackupService;
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] DeleteBackupsCommandSettings settings)
    {
        List<DatabaseBackup> backups = databaseBackupService.GetBackups().ToList();
        int backupNumber = backups.Count;
        if (backupNumber == 0)
        {
            AnsiConsole.MarkupLine("[green]No backups to delete[/]");
            return 0;
        }
        if (settings.All)
        {
            if (AnsiConsole.Confirm($"Do you really want to delete all {backupNumber} backups?"))
            {
                bool successful = databaseBackupService.DeleteAll();
                string response = successful ? "[green]Backups are deleted[/]" : "[red]Something went wrong, please check the log[/]";
                AnsiConsole.MarkupLine(response);
                return successful ? 0 : 1;
            }
            return 0;
        }

        var selection = AnsiConsole.Prompt(new SelectionPrompt<DatabaseBackup>().UseConverter(s => s.DisplayName)
                                                                                .Title("Select backup to delete")
                                                                                .AddChoices(backups));

        if (selection is not null)
        {
            bool success = databaseBackupService.DeleteBackup(selection);
            string response = success ? "[green]Backup was deleted[/]" : "[red]Could not delete backup, please check the log[/]";
            AnsiConsole.MarkupLine(response);
            return success ? 0 : 1;
        }
        AnsiConsole.MarkupLine("[red]Selection was not valid![/]");
        return 1;
    }
}
