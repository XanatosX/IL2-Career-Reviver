namespace IL2CarrerReviverModel.Services;
public abstract class BaseDatabaseConnectionService : IDatabaseConnectionStringService
{
    public string? GetConnectionString()
    {
        string? rawData = GetRawConnectionString();
        if (rawData is null)
        {
            return null;
        }
        return $"Data Source={GetRawConnectionString()}";
    }

    public string? GetDatabasePath()
    {
        return GetRawConnectionString();
    }

    protected abstract string? GetRawConnectionString();
}
