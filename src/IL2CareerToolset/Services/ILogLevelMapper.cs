using IL2CareerToolset.Enums;

namespace IL2CareerToolset.Services;

internal interface ILogLevelMapper<T>
{
    LogSeverity GetLogLevel(T logLevel);

    T GetLogLevel(LogSeverity logLevel);
}
