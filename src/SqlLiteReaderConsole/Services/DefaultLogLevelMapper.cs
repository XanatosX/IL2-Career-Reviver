using IL2CarrerReviverConsole.Enums;
using Microsoft.Extensions.Logging;
using Serilog.Events;

namespace IL2CarrerReviverConsole.Services;
internal class DefaultLogLevelMapper : ILogLevelMapper<LogEventLevel>
{
    private readonly ILogger<DefaultLogLevelMapper> logger;

    public DefaultLogLevelMapper(ILogger<DefaultLogLevelMapper> logger)
    {
        this.logger = logger;
    }

    public LogEventLevel GetLogLevel(LogSeverity logLevel)
    {
        logger.LogDebug($"Convert type {logLevel.GetType()} to {typeof(LogEventLevel)}");
        return logLevel switch
        {
            LogSeverity.Trace => LogEventLevel.Verbose,
            LogSeverity.Debug => LogEventLevel.Debug,
            LogSeverity.Information => LogEventLevel.Information,
            LogSeverity.Warning => LogEventLevel.Warning,
            LogSeverity.Error => LogEventLevel.Error,
            LogSeverity.Critical => LogEventLevel.Fatal,
            _ => LogEventLevel.Verbose
        };
    }

    public LogSeverity GetLogLevel(LogEventLevel logLevel)
    {
        logger.LogDebug($"Convert type {logLevel.GetType()} to {typeof(LogSeverity)}");
        return logLevel switch
        {
            LogEventLevel.Verbose => LogSeverity.Trace,
            LogEventLevel.Debug => LogSeverity.Debug,
            LogEventLevel.Information => LogSeverity.Information,
            LogEventLevel.Warning => LogSeverity.Warning,
            LogEventLevel.Error => LogSeverity.Error,
            LogEventLevel.Fatal => LogSeverity.Critical,
            _ => LogSeverity.Unknown
        };
    }
}
