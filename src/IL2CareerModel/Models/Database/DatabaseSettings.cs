namespace IL2CareerModel.Models.Database;
public class DatabaseSettings
{
    /// <summary>
    /// The connection string to use
    /// </summary>
    public string ConnectionString => $"Data Source={DatabasePath}";

    /// <summary>
    /// The file path to the database
    /// </summary>
    public string? DatabasePath { get; set; }
}
