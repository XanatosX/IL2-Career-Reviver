using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL2CarrerReviverModel.Models;
public class DatabaseBackup
{
    public required Guid Guid { get; set; }

    public required DateTime CreationDate { get; set; }

    public string? BackupName { get; set; }

    public required string BackupPath { get; set; }

    public string? Checksum { get; set; }

    public string DisplayName => BackupName ?? Guid.ToString();
}
