using IL2CareerToolset.Enums;
using System.Text.Json.Serialization;

namespace IL2CareerToolset.Model;
internal class Setting
{
    public string? DatabasePath { get; set; }

    public LogSeverity? LogLevel { get; set; }

    [JsonIgnore]
    public LogSeverity RealLogLevel => LogLevel == LogSeverity.Unknown ? LogSeverity.Warning : LogLevel ?? LogSeverity.Warning;
}
