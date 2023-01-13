using IL2CarrerReviverConsole.Model;

namespace IL2CarrerReviverConsole.Services;
internal interface ISettingsService
{
    Setting? GetSettings();
    void UpdateSettings(Func<Setting, Setting> updateAction);
    void UpdateSettings(Setting newSettings);
}