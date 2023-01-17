using Spectre.Console.Cli;
using System.ComponentModel;

namespace IL2CarrerReviverConsole.Commands.Cli.Settings;
internal class DeleteBackupsCommandSettings : CommandSettings
{
    [CommandOption("-a|--all")]
    [Description("Delete all backups")]
    public bool All { get; set; }

    [CommandArgument(0, "[BACKUP_GUID]")]
    [Description("The backup guid to delete")]
    public string? BackupGuid { get; set; }
}
