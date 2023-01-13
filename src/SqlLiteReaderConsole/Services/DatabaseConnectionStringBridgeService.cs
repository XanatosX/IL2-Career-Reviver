using IL2CarrerReviverConsole.Model;
using IL2CarrerReviverModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL2CarrerReviverConsole.Services;
internal class DatabaseConnectionStringBridgeService : BaseDatabaseConnectionService
{
    private readonly ISettingsService settingsService;

    public DatabaseConnectionStringBridgeService(ISettingsService settingsService)
    {
        this.settingsService = settingsService;
    }

    protected override string? GetRawConnectionString()
    {
        Setting? setting = settingsService.GetSettings();
        if (setting is null | setting?.DatabasePath is null)
        {
            throw new Exception("Missing Database connection string, please set the string first");
        }

        return setting.DatabasePath;
    }
}
