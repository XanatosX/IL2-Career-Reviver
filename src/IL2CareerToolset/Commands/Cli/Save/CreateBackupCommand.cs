using IL2CareerModel.Services;
using IL2CareerToolset.Services;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace IL2CareerToolset.Commands.Cli.Save;

internal class CreateBackupCommandSettings : CommandSettings
{
    [CommandArgument(0, "[BACKUP_NAME]")]
    [Description("The name of the backup to create")]
    public string? BackupName { get; init; }
}

[Description("Create a backup from your savegame's")]
internal class CreateBackupCommand : Command<CreateBackupCommandSettings>
{
    private readonly ISettingsService settingsService;
    private readonly IDatabaseBackupService databaseBackupService;
    private readonly ILogger<CreateBackupCommand> logger;

    public CreateBackupCommand(ISettingsService settingsService, IDatabaseBackupService databaseBackupService, ILogger<CreateBackupCommand> logger)
    {
        this.settingsService = settingsService;
        this.databaseBackupService = databaseBackupService;
        this.logger = logger;
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] CreateBackupCommandSettings settings)
    {
        var database = settingsService.GetSettings()?.DatabasePath;
        if (database is null)
        {
            AnsiConsole.MarkupLine("[red]There is no database path provided please call 'settings auto' or 'settings manual' first[/]");
            return 1;
        }

        string backupName = settings.BackupName ?? $"Manually generated - {DateTime.Now}";
        if (settings.BackupName is null && AnsiConsole.Confirm($"Do you want to name your backup or use the name [yellow]{backupName}[/]?"))
        {
            backupName = AnsiConsole.Ask<string>("Enter backup name: ");
        }
        var backup = databaseBackupService.CreateBackup(backupName);
        if (backup is null)
        {
            AnsiConsole.MarkupLine("[red]Something went wrong while creating the backup, please check the log for more information[/]");
            return 1;
        }
        AnsiConsole.MarkupLine($"[green]Backup with name [/][yellow]{backupName}[/][green] was created successfully[/]");
        return 0;
    }
}
