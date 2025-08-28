namespace IL2CareerModel.Services;
internal class DefaultGamePathValidationService : IGamePathValidationService
{
    private const string ROOT_FOLDER_SUB_PATH = "data/Career/cp.db";

    public string? GetPathToDatabase(string gameFolderPath)
    {
        string fullPath = BuildGamePath(gameFolderPath);
        return IsValidGamePath(gameFolderPath) ? fullPath : null;
    }

    private string BuildGamePath(string gameFolderPath)
    {
        return Path.Combine(gameFolderPath, ROOT_FOLDER_SUB_PATH);
    }

    public bool IsValidGamePath(string gameFolderPath)
    {
        string fullPath = BuildGamePath(gameFolderPath);
        return File.Exists(fullPath);
    }
}
