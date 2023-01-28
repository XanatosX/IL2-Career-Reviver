using IL2CareerModel.Models.Database;
using IL2CareerModel.Services;
using IL2CareerToolset.Commands.Cli.Settings;
using IL2CareerToolset.Services;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace IL2CareerToolset.Commands.Cli.Save;

[Description("Restore a previously created backup")]
internal class RestoreBackupCommand : Command<RestoreBackupCommandSettings>
{
    private readonly IDatabaseBackupService databaseBackupService;
    private readonly ILogger<RestoreBackupCommand> logger;

    public RestoreBackupCommand(IDatabaseBackupService databaseBackupService, ILogger<RestoreBackupCommand> logger)
    {
        this.databaseBackupService = databaseBackupService;
        this.logger = logger;
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] RestoreBackupCommandSettings settings)
    {
        if (settings.Guid is not null)
        {
            var backupToRestore = databaseBackupService.GetBackups()
                                                       .Where(backup => backup.Guid == settings.Guid)
                                                       .FirstOrDefault();
            if (backupToRestore is null)
            {
                AnsiConsole.MarkupLine($"[red]Did not find any backup for guid {settings.Guid}[/]");
                return 1;
            }

            return RestoreBackup(backupToRestore);
        }

        List<DatabaseBackup> backups = databaseBackupService.GetBackups()
                                                            .Where(databaseBackupService.IsValidBackup)
                                                            .ToList();
        if (backups.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]Could not find any backup to restore![/]");
            return 1;
        }

        var selectedBackup = AnsiConsole.Prompt(new SelectionPrompt<DatabaseBackup>().UseConverter(backup => backup.DisplayName)
                                                                                     .Title("Select backup for restoring")
                                                                                     .AddChoices(backups));

        if (selectedBackup is null)
        {
            AnsiConsole.MarkupLine("[red]No backup was selected for restoring[/]");
            return 1;
        }

        return RestoreBackup(selectedBackup);
    }

    private int RestoreBackup(DatabaseBackup backupToRestore)
    {
        Guid guid = backupToRestore.Guid;
        logger.LogInformation("Create additional backup before restoring!");
        var safetyBackup = databaseBackupService.CreateBackup($"Automatically created before restore - {DateTime.Now}");
        if (safetyBackup is null)
        {
            AnsiConsole.MarkupLine($"[red]Could not create safty backup, please check the log[/]");
            return 1;
        }

        bool success = databaseBackupService.RestoreBackup(backupToRestore);
        string information = success ? $"[green]Backup {guid} restored successfully[/]" : "[red]Backup was not restored properly please restore it manually[/]";
        AnsiConsole.MarkupLine(information);
        return success ? 0 : 1;
    }
}
