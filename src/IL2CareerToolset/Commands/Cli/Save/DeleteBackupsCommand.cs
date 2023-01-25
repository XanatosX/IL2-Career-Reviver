using IL2CareerModel.Models.Database;
using IL2CarrerReviverConsole.Commands.Cli.Settings;
using IL2CarrerReviverConsole.Services;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Intrinsics.X86;

namespace IL2CarrerReviverConsole.Commands.Cli.Save;

[Description("Delete a single or multiple backups")]
internal class DeleteBackupsCommand : Command<DeleteBackupsCommandSettings>
{
    private readonly IDatabaseBackupService databaseBackupService;
    private readonly ILogger<DeleteBackupsCommand> logger;

    public DeleteBackupsCommand(IDatabaseBackupService databaseBackupService, ILogger<DeleteBackupsCommand> logger)
    {
        this.databaseBackupService = databaseBackupService;
        this.logger = logger;
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] DeleteBackupsCommandSettings settings)
    {
        List<DatabaseBackup> backups = databaseBackupService.GetBackups().OrderBy(b => b.CreationDate).ToList();
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

        if (settings.BackupGuid is not null)
        {
            Guid guid = new Guid(settings.BackupGuid);
            var backupToDelete = backups.Find(b => b.Guid == guid);
            if (backupToDelete is null)
            {
                AnsiConsole.MarkupLine($"[red]Could not find a backup for guid {guid}[/]");
                return 1;
            }

            return DeleteBackup(backupToDelete);
        }

        var selection = AnsiConsole.Prompt(new SelectionPrompt<DatabaseBackup>().UseConverter(s => $"{s.DisplayName} - {s.CreationDate.ToString()}")
                                                                                .Title("Select backup to delete")
                                                                                .AddChoices(backups));

        if (selection is not null)
        {
            if (AnsiConsole.Confirm($"Do you really want to delete the backup [yellow]{selection.DisplayName}[/]?"))
            {
                return DeleteBackup(selection);
            }
            return 0;
        }
        AnsiConsole.MarkupLine("[red]Selection was not valid![/]");
        return 1;
    }

    private int DeleteBackup(DatabaseBackup? selection)
    {
        bool success = databaseBackupService.DeleteBackup(selection);
        string response = success ? "[green]Backup was deleted[/]" : "[red]Could not delete backup, please check the log[/]";
        AnsiConsole.MarkupLine(response);
        return success ? 0 : 1;
    }
}
