using Spectre.Console.Cli;
using System.ComponentModel;

namespace IL2CareerToolset.Commands.Cli.Settings;
internal class ChangeBackupNameSettings : CommandSettings
{
    [CommandArgument(0, "[BACKUP_GUID]")]
    [Description("The guid of the backup to rename")]
    public Guid? Guid { get; set; }

    [CommandArgument(1, "[NEW_NAME]")]
    [Description("The new name to use for the backup")]
    public string? Name { get; set; }
}
