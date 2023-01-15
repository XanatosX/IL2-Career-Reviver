using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL2CarrerReviverConsole.Commands.Cli.Settings;
internal class ManuellDatabaseCommandSettings : CommandSettings
{
    [CommandArgument(0, "[GameRootFolder]")]
    [Description("Path to the root folder of the game")]
    public string? GameRootFolder { get; set; }

    public bool IsInteractiv => GameRootFolder is null;
}
