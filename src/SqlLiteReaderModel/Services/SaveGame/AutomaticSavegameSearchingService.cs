using IL2CarrerReviverConsole.Services;

namespace IL2CarrerReviverModel.Services.SaveGame;

internal class AutomaticSteamSavegameSearchingService : ISavegameLocatorService
{
    private const string FOLDER_TO_SEARCH = @"steamapps\common";

    private const string GAME_FOLDER_TO_SEARCH = @"il-2 sturmovik";

    public string DisplayName => "Automated Steam search";

    public int Priority => 1;

    private readonly string[] ignoredFolders = new string[] {
        "users",
        "my games",
        "windows",
        "temp",
        "onedrive",
        "downloads"
    };
    private readonly IGamePathValidationService gamePathValidationService;

    public AutomaticSteamSavegameSearchingService(IGamePathValidationService gamePathValidationService)
    {
        this.gamePathValidationService = gamePathValidationService;
    }

    public string? GetSavegamePath()
    {
        DriveInfo[] drives = DriveInfo.GetDrives();
        List<Task<string>> awaitedTasks = new List<Task<string>>();
        foreach (DriveInfo drive in drives)
        {
            awaitedTasks.Add(ScanDiscForSteamApps(drive, 5));
        }
        Task.WaitAll(awaitedTasks.ToArray());
        string[] folders = awaitedTasks.Select(t => t.Result).ToArray();
        string returnData = string.Empty;
        foreach (string folder in folders)
        {
            string[] games = Directory.GetDirectories(folder).Select(game => new DirectoryInfo(game).Name).ToArray();
            string gameFolder = games.FirstOrDefault(game => game.ToLower().StartsWith(GAME_FOLDER_TO_SEARCH), string.Empty);
            if (!string.IsNullOrEmpty(gameFolder))
            {
                returnData = Path.Combine(folder, gameFolder);
                break;
            }
        }
        return gamePathValidationService.GetPathToDatabase(returnData);
    }

    private async Task<string> ScanDiscForSteamApps(DriveInfo info, int depth)
    {
        return await Task.Run(() =>
        {
            return ScanForSteamFolder(info.Name, 0, depth);
        });
    }

    private string ScanForSteamFolder(string root, int currentDepth, int depth)
    {
        currentDepth++;
        string[] directories = new string[0];
        try
        {
            directories = Directory.GetDirectories(root).Where(dir => !ignoredFolders.Contains(dir.ToLower())).ToArray();
        }
        catch (Exception)
        {
            return string.Empty; ;
        }

        string? returnDirectory = directories?.FirstOrDefault(dir => dir != null && dir.Contains(FOLDER_TO_SEARCH));
        if (string.IsNullOrEmpty(returnDirectory) && directories != null && currentDepth < depth)
        {
            foreach (string directory in directories)
            {
                string subScan = ScanForSteamFolder(directory, currentDepth, depth);
                if (!string.IsNullOrEmpty(subScan))
                {
                    returnDirectory = subScan;
                    break;
                }
            }
        }
        return returnDirectory ?? string.Empty;
    }
}
