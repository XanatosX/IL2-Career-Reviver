using IL2CarrerReviverConsole.Model;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace IL2CarrerReviverConsole.Services;
internal class SettingsService : ISettingsService
{
    private readonly PathService pathService;
    private readonly ILogger<SettingsService> logger;

    public SettingsService(PathService pathService, ILogger<SettingsService> logger)
    {
        this.pathService = pathService;
        this.logger = logger;
    }

    public Setting? GetSettings()
    {
        Setting? setting = null;
        if (!File.Exists(pathService.GetSettingsPath()))
        {
            return setting;
        }
        using (FileStream stream = new FileStream(pathService.GetSettingsPath(), FileMode.Open, FileAccess.Read))
        {
            setting = JsonSerializer.Deserialize<Setting>(stream);
        }
        return setting;
    }

    public bool UpdateSettings(Setting newSettings)
    {
        return SaveSettings(newSettings);
    }

    public bool UpdateSettings(Action<Setting> updateAction)
    {
        Setting setting = GetSettings() ?? new Setting();
        updateAction(setting);
        return SaveSettings(setting);
    }

    private bool SaveSettings(Setting setting)
    {
        bool saveComplete = true;
        using (FileStream fileStream = new FileStream(pathService.GetSettingsPath(), FileMode.Create, FileAccess.Write))
        {
            try
            {
                JsonSerializer.Serialize(fileStream, setting);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Saving the settings did not work");
                saveComplete = false;
            }
        }
        return saveComplete;
    }
}
