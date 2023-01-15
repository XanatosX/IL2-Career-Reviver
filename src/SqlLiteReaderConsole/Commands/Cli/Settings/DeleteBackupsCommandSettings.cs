using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL2CarrerReviverConsole.Commands.Cli.Settings;
internal class DeleteBackupsCommandSettings : CommandSettings
{
    [CommandOption("-a|--all")]
    [Description("Delete all backups")]
    public bool All { get; set; }
}
