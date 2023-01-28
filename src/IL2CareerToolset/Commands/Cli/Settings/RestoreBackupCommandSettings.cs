using Spectre.Console.Cli;
using System.ComponentModel;

namespace IL2CareerToolset.Commands.Cli.Settings;
internal class RestoreBackupCommandSettings : CommandSettings
{
    [CommandArgument(0, "[GUID]")]
    [Description("The guid of the backup to restore")]
    public Guid? Guid { get; set; }
}
