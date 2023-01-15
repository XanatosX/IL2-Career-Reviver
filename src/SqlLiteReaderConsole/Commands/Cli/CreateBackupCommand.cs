using IL2CarrerReviverConsole.Services;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL2CarrerReviverConsole.Commands.Cli;
internal class CreateBackupCommand : Command
{
    private readonly ISettingsService settingsService;
    private readonly IDatabaseBackupService databaseBackupService;
    private readonly ILogger<CreateBackupCommand> logger;

    public CreateBackupCommand(ISettingsService settingsService, IDatabaseBackupService databaseBackupService, ILogger<CreateBackupCommand> logger)
    {
        this.settingsService = settingsService;
        this.databaseBackupService = databaseBackupService;
        this.logger = logger;
    }

    public override int Execute(CommandContext context)
    {
        var database = settingsService.GetSettings()?.DatabasePath;
        if (database is null)
        {
            AnsiConsole.MarkupLine("[red]There is no database path provided please call 'settings auto' or 'settings manuell' first[/]");
            return 1;
        }
        string? backupName = null;
        if (AnsiConsole.Confirm("Do you want to name your backup?"))
        {
            backupName = AnsiConsole.Ask<string>("Enter backup name: ");
        }

        var backup = databaseBackupService.CreateBackup(backupName);
        if (backup is null)
        {
            AnsiConsole.MarkupLine("[red]Something went wrong while creating the backup, please check the log for more information[/]");
            return 1;
        }
        AnsiConsole.MarkupLine("[green]Backup was created successfully[/]");
        return 0;
    }
}
