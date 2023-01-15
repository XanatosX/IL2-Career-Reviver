using IL2CarrerReviverConsole.Enums;
using System.Text.Json.Serialization;

namespace IL2CarrerReviverConsole.Model;
internal class Setting
{
    public string? DatabasePath { get; set; }

    public LogSeverity? LogLevel { get; set; }

    [JsonIgnore]
    public LogSeverity RealLogLevel => LogLevel == LogSeverity.Unknown ? LogSeverity.Warning : LogLevel ?? LogSeverity.Warning;
}
