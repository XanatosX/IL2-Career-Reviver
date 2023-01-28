namespace IL2CareerModel.Services.SaveGame;

public interface ISavegameLocatorService
{
    string DisplayName { get; }

    int Priority { get; }

    string? GetSavegamePath();
}
