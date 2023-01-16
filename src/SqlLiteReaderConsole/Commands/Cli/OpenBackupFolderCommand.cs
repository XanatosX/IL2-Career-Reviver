using IL2CarrerReviverConsole.Services;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL2CarrerReviverConsole.Commands.Cli;
internal class OpenBackupFolderCommand : Command
{
    private readonly IDatabaseBackupService databaseBackupService;

    public OpenBackupFolderCommand(IDatabaseBackupService databaseBackupService)
    {
        this.databaseBackupService = databaseBackupService;
    }

    public override int Execute(CommandContext context)
    {
        //Process.Start(databaseBackupService.GetBackupFolder());
        return 0;
    }
}
