using IL2CareerModel.Models.Database;
using IL2CarrerReviverModel.Services;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace IL2CarrerReviverConsole.Services;
internal class DatabaseBackupService : IDatabaseBackupService
{
    private readonly IFileChecksumService fileChecksumService;

    private readonly ILogger<DatabaseBackupService> logger;

    private readonly string backupTargetFolder;

    private readonly string backupOverviewFile;

    private readonly string sourceDatabaseFile;

    public DatabaseBackupService(ISettingsFolderBridge pathService,
                                 IFileChecksumService fileChecksumService,
                                 DatabaseSettings databaseSettings,
                                 ILogger<DatabaseBackupService> logger)
    {
        this.fileChecksumService = fileChecksumService;
        this.logger = logger;
        string baseFolder = pathService.GetSettingsFolder();
        backupTargetFolder = Path.Combine(baseFolder, "backups");
        backupOverviewFile = Path.Combine(backupTargetFolder, "backups.json");
        if (!Directory.Exists(backupTargetFolder))
        {
            Directory.CreateDirectory(backupTargetFolder);
        }
        sourceDatabaseFile = databaseSettings.DatabasePath ?? string.Empty;
    }

    public string GetBackupFolder()
    {
        return backupTargetFolder;
    }

    public DatabaseBackup? CreateBackup() => CreateBackup(null);

    public DatabaseBackup? CreateBackup(string? name)
    {
        if (!File.Exists(sourceDatabaseFile))
        {
            return null;
        }
        DateTime time = DateTime.Now;
        DatabaseBackup backup = new DatabaseBackup
        {
            Guid = Guid.NewGuid(),
            BackupName = name,
            BackupPath = Path.Combine(backupTargetFolder, $"backup_{time.ToString("yyyyMMdd_HHmmss")}"),
            CreationDate = time,
        };

        backup.Checksum = fileChecksumService.GetChecksum(sourceDatabaseFile);

        List<DatabaseBackup> backups = GetBackups().ToList();
        backups.Add(backup);

        File.Copy(sourceDatabaseFile, backup.BackupPath);

        if (fileChecksumService.GetChecksum(backup.BackupPath) != backup.Checksum)
        {
            logger.LogCritical("Backup failed, something went wrong while creating a copy!");
            File.Delete(backup.BackupPath);
            return null;
        }

        SaveBackups(backups);


        return backup;
    }

    private void SaveBackups(IEnumerable<DatabaseBackup> backups)
    {
        using (FileStream fileStream = new FileStream(backupOverviewFile, FileMode.Create, FileAccess.Write))
        {
            try
            {
                logger.LogDebug("Saving backup list");
                JsonSerializer.Serialize(fileStream, backups);
            }
            catch (Exception e)
            {
                logger.LogCritical(e, "Something went wrong while creating backup overview file!");
            }
        }
    }

    public IEnumerable<DatabaseBackup> GetBackups()
    {
        IEnumerable<DatabaseBackup> returnBackups = Enumerable.Empty<DatabaseBackup>();
        if (!File.Exists(backupOverviewFile))
        {
            return returnBackups;
        }
        using (FileStream fileStream = new FileStream(backupOverviewFile, FileMode.Open, FileAccess.Read))
        {
            try
            {
                returnBackups = JsonSerializer.Deserialize<IEnumerable<DatabaseBackup>>(fileStream) ?? Enumerable.Empty<DatabaseBackup>();
            }
            catch (Exception e)
            {
                logger.LogCritical(e, "Something went wrong while reading backup overview file!");
            }
        }
        return returnBackups;
    }

    public bool RestoreBackup(DatabaseBackup databaseBackup)
    {
        File.Copy(databaseBackup.BackupPath, sourceDatabaseFile, true);
        return fileChecksumService.GetChecksum(sourceDatabaseFile) == databaseBackup.Checksum;
    }

    public bool DeleteBackup(DatabaseBackup databaseBackup)
    {
        DatabaseBackup? backup = GetBackups().Where(b => b.Guid == databaseBackup.Guid).FirstOrDefault();
        if (backup is null)
        {
            logger.LogWarning($"Could not find backup for searched guid {databaseBackup.Guid}");
            return false;
        }

        File.Delete(backup.BackupPath);
        if (File.Exists(backup.BackupPath))
        {
            logger.LogWarning($"Could not delete backup");
            return false;
        }
        SaveBackups(GetBackups().Where(b => b.Guid != databaseBackup.Guid));
        return true;
    }

    public bool DeleteAll()
    {
        Directory.Delete(backupTargetFolder, true);
        return Directory.Exists(backupTargetFolder);
    }

    public bool UpdateBackupName(Guid id, string name)
    {
        var allBackups = GetBackups().ToList();
        var backup = allBackups.Where(b => b.Guid == id).FirstOrDefault();
        if (backup is null)
        {
            logger.LogWarning($"Could not find backup with id {id} for renaming");
            return false;
        }
        backup.BackupName = name;
        var backups = allBackups.Where(b => b.Guid != id).ToList();
        logger.LogInformation("Edit list for backups now");
        backups.Add(backup);
        SaveBackups(backups.ToList());

        return GetBackups().Where(b => b.Guid == id).FirstOrDefault()?.BackupName == name;
    }

    public bool IsValidBackup(DatabaseBackup backup)
    {
        return fileChecksumService.GetChecksum(backup.BackupPath) == backup.Checksum;
    }
}
