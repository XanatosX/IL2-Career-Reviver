using IL2CareerToolset.Commands.Cli.Settings;
using IL2CareerToolset.Enums;
using IL2CareerToolset.Services;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL2CareerToolset.Commands.Cli;
[Description("Set the log level for the application. If you set it on Information every log for information and above will be shown")]
internal class SetLogLevelCommand : Command<SetLogLevelCommandSettings>
{
    private readonly ISettingsService settingsService;
    private readonly DefaultLogLevelMapper defaultLogLevelMapper;
    private readonly ILogger<SetLogLevelCommand> logger;

    public SetLogLevelCommand(ISettingsService settingsService, DefaultLogLevelMapper defaultLogLevelMapper, ILogger<SetLogLevelCommand> logger)
    {
        this.settingsService = settingsService;
        this.defaultLogLevelMapper = defaultLogLevelMapper;
        this.logger = logger;
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] SetLogLevelCommandSettings settings)
    {
        logger.LogInformation($"Set log level to {settings.LogLevel}");
        settingsService.UpdateSettings(storedSettings =>
        {
            storedSettings.LogLevel = settings.LogLevel;
        });

        var loadedSettings = settingsService.GetSettings();
        if (loadedSettings?.RealLogLevel == settings.LogLevel)
        {
            AnsiConsole.MarkupLine($"[green]Set loglevel to {settings.LogLevel} succesfully[/]");
            return 0;
        }
        AnsiConsole.MarkupLine($"[green]Failed to set loglevel to {settings.LogLevel}[/]");
        return 1;
    }
}
