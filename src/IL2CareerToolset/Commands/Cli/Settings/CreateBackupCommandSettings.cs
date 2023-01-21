using Spectre.Console.Cli;

namespace IL2CarrerReviverConsole.Commands.Cli.Settings;

internal class CreateBackupCommandSettings : CommandSettings
{
    [CommandArgument(0, "[BACKUP_NAME]")]
    public string? BackupName { get; init; }
}