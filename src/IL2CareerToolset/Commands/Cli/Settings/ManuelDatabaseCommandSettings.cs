using Spectre.Console.Cli;
using System.ComponentModel;

namespace IL2CareerToolset.Commands.Cli.Settings;
internal class ManuelDatabaseCommandSettings : CommandSettings
{
    [CommandArgument(0, "[GameRootFolder]")]
    [Description("Path to the root folder of the game")]
    public string? GameRootFolder { get; set; }

    public bool IsInteractiv => GameRootFolder is null;
}
