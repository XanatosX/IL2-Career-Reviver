using Spectre.Console.Cli;
using System.ComponentModel;

namespace IL2CarrerReviverConsole.Commands.Cli.Settings;
internal class GetPilotSettings : CommandSettings
{
    [CommandArgument(0, "[Pilot Name]")]
    [Description("The name of the pilot to search")]
    public string? Name { get; set; }

    [CommandOption("-p|--player")]
    [Description("Show player only pilots")]
    public bool PlayerOnly { get; set; }
}
