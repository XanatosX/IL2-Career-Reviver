﻿using Spectre.Console.Cli;
using System.ComponentModel;

namespace IL2CarrerReviverConsole.Commands.Cli.Settings;
internal class ManuellDatabaseCommandSettings : CommandSettings
{
    [CommandArgument(0, "[GameRootFolder]")]
    [Description("Path to the root folder of the game")]
    public string? GameRootFolder { get; set; }

    public bool IsInteractiv => GameRootFolder is null;
}