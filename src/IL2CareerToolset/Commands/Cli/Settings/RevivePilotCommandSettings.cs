using Spectre.Console.Cli;
using System.ComponentModel;

namespace IL2CareerToolset.Commands.Cli.Settings;

[Description("Command to revive a pilot of the campain")]
internal class RevivePilotCommandSettings : CommandSettings
{
    [CommandOption("-i|--ironman")]
    [Description("Include iron man characters")]
    public bool IncludeIronMan { get; set; }
}
