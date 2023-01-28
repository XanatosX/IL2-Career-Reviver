namespace IL2CareerModel.Models.Database;
public class DatabaseBackup
{
    public required Guid Guid { get; set; }

    public required DateTime CreationDate { get; set; }

    public string? BackupName { get; set; }

    public required string BackupPath { get; set; }

    public string? Checksum { get; set; }

    public string DisplayName => BackupName ?? Guid.ToString();
}
