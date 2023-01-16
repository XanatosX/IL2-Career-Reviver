using IL2CarrerReviverConsole.Services;
using IL2CarrerReviverConsole.Views;
using IL2CarrerReviverModel.Models;
using IL2CarrerReviverModel.Services;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL2CarrerReviverConsole.Commands.Cli;
internal class ListBackupsCommand : Command
{
    private readonly ViewFactory viewFactory;
    private readonly IDatabaseBackupService backupService;
    private readonly ILogger<ListBackupsCommand> logger;

    public ListBackupsCommand(ViewFactory viewFactory, IDatabaseBackupService backupService, ILogger<ListBackupsCommand> logger)
    {
        this.viewFactory = viewFactory;
        this.backupService = backupService;
        this.logger = logger;
    }

    public override int Execute(CommandContext context)
    {
        List<DatabaseBackup> backups = backupService.GetBackups().OrderBy(b => b.CreationDate).ToList();
        if (backups.Count == 0)
        {
            AnsiConsole.MarkupLine($"[green]No backups found for listing[/]");
            return 0;
        }
        var view = viewFactory.CreateView<BackupTableView>();
        if (view is null)
        {
            string errorMessage = "Could not find view for BackupTableView";
            logger.LogError(errorMessage);
            AnsiConsole.MarkupLine($"[red]{errorMessage}[/]");
            return 1;
        }

        AnsiConsole.MarkupLine($"Backups are located at {backupService.GetBackupFolder()}");
        AnsiConsole.Write(view.GetView(backups));
        return 0;
    }
}
