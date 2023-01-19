using Microsoft.Extensions.Logging;
using System.Text;

namespace IL2CarrerReviverModel.Services;
internal sealed class DefaultByteArrayToDateTimeService : IByteArrayToDateTimeService
{
    private readonly ILogger<DefaultByteArrayToDateTimeService> logger;

    public DefaultByteArrayToDateTimeService(ILogger<DefaultByteArrayToDateTimeService> logger)
    {
        this.logger = logger;
    }

    public DateTime? GetDateTime(byte[] bytes)
    {
        DateTime? returnTime = null;
        if (bytes.Length == 0)
        {
            return returnTime;
        }
        string translated = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
        logger.LogInformation($"Parse byte date with string representation to date time {translated}");
        try
        {
            returnTime = DateTime.Parse(translated);
        }
        catch (Exception e)
        {
            logger.LogWarning(e, $"Could not parse byte array with string representation {translated}");
        }
        return returnTime;
    }
}
