using IL2CarrerReviverConsole.Model;

namespace IL2CarrerReviverConsole.Services;
internal interface ISettingsService
{
    Setting? GetSettings();
    bool UpdateSettings(Action<Setting> updateAction);
    bool UpdateSettings(Setting newSettings);
}