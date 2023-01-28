namespace IL2CareerModel.Services;
public interface IGamePathValidationService
{
    string? GetPathToDatabase(string gameFolderPath);
    bool IsValidGamePath(string gameFolderPath);
}
