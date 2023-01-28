using IL2CareerToolset.Services;
using Microsoft.Extensions.Logging;

namespace IL2CarrerReviverModel.Services.SaveGame;

internal class AutomaticSteamSavegameSearchingService : ISavegameLocatorService
{
    private const string FOLDER_TO_SEARCH = @"steamapps\common";

    private const string GAME_FOLDER_TO_SEARCH = @"il-2 sturmovik";

    public string DisplayName => "Automated Steam search";

    public int Priority => 1;

    private readonly string[] ignoredFolders = new[] {
        "users",
        "my games",
        "windows",
        "temp",
        "onedrive",
        "downloads",
        "$recycle.bin",
        "wudownloadcache",
        "wpsystem"
    };
    private readonly IGamePathValidationService gamePathValidationService;

    private readonly ILogger<AutomaticSteamSavegameSearchingService> logger;

    public AutomaticSteamSavegameSearchingService(IGamePathValidationService gamePathValidationService, ILogger<AutomaticSteamSavegameSearchingService> logger)
    {
        this.gamePathValidationService = gamePathValidationService;
        this.logger = logger;
    }

    public string? GetSavegamePath()
    {
        DriveInfo[] drives = DriveInfo.GetDrives();
        List<Task<string>> awaitedTasks = new();
        logger.LogInformation($"Start scan");
        foreach (DriveInfo drive in drives)
        {
            awaitedTasks.Add(ScanDiscForSteamApps(drive, 5));
        }
        logger.LogInformation($"Waiting for scan to complete");
        Task.WaitAll(awaitedTasks.ToArray());
        string[] folders = awaitedTasks.Select(t => t.Result).ToArray();
        string returnData = string.Empty;
        logger.LogInformation($"Check {folders.Length} folders if a IL-2 installation can be found");
        foreach (string folder in folders)
        {
            string[] games = Directory.GetDirectories(folder).Select(game => new DirectoryInfo(game).Name).ToArray();
            string gameFolder = games.FirstOrDefault(game => game.ToLower().StartsWith(GAME_FOLDER_TO_SEARCH), string.Empty);
            if (!string.IsNullOrEmpty(gameFolder))
            {
                logger.LogInformation($"Found game folder on {gameFolder}");
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
            logger.LogInformation($"Scanning disc {info.Name}");
            return ScanForSteamFolder(info.Name, 0, depth);
        });
    }

    private string ScanForSteamFolder(string root, int currentDepth, int depth)
    {
        currentDepth++;
        string[] directories = Array.Empty<string>();
        logger.LogInformation($"Scan folder {root}");
        try
        {
            directories = Directory.GetDirectories(root).Where(dir => !ignoredFolders.Any(ignored => dir.Contains(ignored, StringComparison.OrdinalIgnoreCase))).ToArray();

            logger.LogInformation($"Found {directories.Length} sub folders for {root} which are getting scanned next");
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Could not get directories for {root}");
            return string.Empty;
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
