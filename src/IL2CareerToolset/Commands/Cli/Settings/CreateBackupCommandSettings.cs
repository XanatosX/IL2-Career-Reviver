using Spectre.Console.Cli;
using System.ComponentModel;

namespace IL2CareerToolset.Commands.Cli.Settings;

internal class CreateBackupCommandSettings : CommandSettings
{
    [CommandArgument(0, "[BACKUP_NAME]")]
    [Description("The name of the backup to create")]
    public string? BackupName { get; init; }
}