using IL2CarrerReviverConsole.Model;
using IL2CarrerReviverModel.Services;
using Microsoft.Extensions.Logging;

namespace IL2CarrerReviverConsole.Services;
internal class DatabaseConnectionStringBridgeService : BaseDatabaseConnectionService
{
    private readonly ISettingsService settingsService;
    private readonly ILogger<DatabaseConnectionStringBridgeService> logger;

    public DatabaseConnectionStringBridgeService(ISettingsService settingsService, ILogger<DatabaseConnectionStringBridgeService> logger)
    {
        this.settingsService = settingsService;
        this.logger = logger;
    }

    protected override string? GetRawConnectionString()
    {
        Setting? setting = settingsService.GetSettings();
        if (setting is null | setting?.DatabasePath is null)
        {
            Exception e = new Exception("Missing Database connection string, please set the string first");
            logger.LogCritical(e, "No database string provided");
            throw e;
        }

        return setting.DatabasePath;
    }
}
