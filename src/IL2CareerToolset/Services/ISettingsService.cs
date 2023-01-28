using IL2CareerToolset.Model;

namespace IL2CareerToolset.Services;
internal interface ISettingsService
{
    Setting? GetSettings();
    bool UpdateSettings(Action<Setting> updateAction);
    bool UpdateSettings(Setting newSettings);
}