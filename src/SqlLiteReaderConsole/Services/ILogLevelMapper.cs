using IL2CarrerReviverConsole.Enums;

namespace IL2CarrerReviverConsole.Services;

internal interface ILogLevelMapper<T>
{
    LogSeverity GetLogLevel(T logLevel);

    T GetLogLevel(LogSeverity logLevel);
}
