using IL2CareerModel.Services;

namespace IL2CareerToolset.Services;
internal class SettingsFolderBridge : ISettingsFolderBridge
{
    private readonly PathService pathService;

    public SettingsFolderBridge(PathService pathService)
    {
        this.pathService = pathService;
    }

    public string GetSettingsFolder()
    {
        return pathService.GetAndCreateSettingFolder();
    }
}
