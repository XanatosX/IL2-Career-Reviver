namespace IL2CareerToolset.Services;
internal class PathService
{
    private readonly string settingPath;

    private const string SETTING_FILE_NAME = "settings.json";

    public PathService()
    {
        settingPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AppDomain.CurrentDomain.FriendlyName);
    }

    public string GetAndCreateSettingFolder()
    {
        if (!Directory.Exists(settingPath))
        {
            Directory.CreateDirectory(settingPath);
        }
        return settingPath;
    }

    public string GetSettingsPath()
    {
        return Path.Combine(GetAndCreateSettingFolder(), SETTING_FILE_NAME);
    }
}
