using IL2CarrerReviverModel.Models;

namespace IL2CarrerReviverConsole.Services;
public interface IDatabaseBackupService
{
    string GetBackupFolder();
    DatabaseBackup? CreateBackup();
    DatabaseBackup? CreateBackup(string? name);
    IEnumerable<DatabaseBackup> GetBackups();
    bool RestoreBackup(DatabaseBackup databaseBackup);

    bool DeleteBackup(DatabaseBackup databaseBackup);

    bool DeleteAll();
}