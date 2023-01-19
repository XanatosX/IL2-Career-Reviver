using IL2CarrerReviverConsole.Model;

namespace IL2CarrerReviverConsole.Services;
internal interface ISettingsService
{
    Setting? GetSettings();
    void UpdateSettings(Action<Setting> updateAction);
    void UpdateSettings(Setting newSettings);
}