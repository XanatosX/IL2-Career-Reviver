using IL2CareerModel.Models.Database;

namespace IL2CareerModel.Services;
public interface IDatabaseBackupService
{
    string GetBackupFolder();
    DatabaseBackup? CreateBackup();
    DatabaseBackup? CreateBackup(string? name);
    IEnumerable<DatabaseBackup> GetBackups();

    bool UpdateBackupName(DatabaseBackup backup, string name) => UpdateBackupName(backup.Guid, name);

    bool UpdateBackupName(Guid id, string name);

    bool IsValidBackup(DatabaseBackup backup);

    bool RestoreBackup(DatabaseBackup databaseBackup);

    bool DeleteBackup(DatabaseBackup databaseBackup);

    bool DeleteAll();
}