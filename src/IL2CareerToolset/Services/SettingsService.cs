using IL2CarrerReviverConsole.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IL2CarrerReviverConsole.Services;
internal class SettingsService : ISettingsService
{
    private readonly PathService pathService;

    public SettingsService(PathService pathService)
    {
        this.pathService = pathService;
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

    public void UpdateSettings(Setting newSettings)
    {

    }
    public void UpdateSettings(Action<Setting> updateAction)
    {
        Setting setting = GetSettings() ?? new Setting();
        updateAction(setting);
        SaveSettings(setting);
    }

    private void SaveSettings(Setting setting)
    {
        var test = pathService.GetSettingsPath();
        using (FileStream fileStream = new FileStream(pathService.GetSettingsPath(), FileMode.Create, FileAccess.Write))
        {
            JsonSerializer.Serialize(fileStream, setting);
        }

    }
}
