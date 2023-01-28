using IL2CareerToolset.Enums;
using Serilog.Events;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace IL2CareerToolset.Commands.Cli.Settings;

internal class SetLogLevelCommandSettings : CommandSettings
{
    private const string POSSIBLE_LOG_LEVELS = "Trace, Debug, Information, Warning, Error, Critical";

    [CommandArgument(0, "<LOG_LEVEL>")]
    [Description($"The log level to use for the program. Possible values are {POSSIBLE_LOG_LEVELS}")]
    public LogSeverity LogLevel { get; set; }
}