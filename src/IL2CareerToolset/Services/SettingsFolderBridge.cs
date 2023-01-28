using IL2CareerModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
