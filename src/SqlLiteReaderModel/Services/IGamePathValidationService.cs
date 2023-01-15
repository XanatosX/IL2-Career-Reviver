namespace IL2CarrerReviverConsole.Services;
public interface IGamePathValidationService
{
    string? GetPathToDatabase(string gameFolderPath);
    bool IsValidGamePath(string gameFolderPath);
}
