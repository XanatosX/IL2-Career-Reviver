using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IL2CarrerReviverModel.Models;

[Table("config")]
[Index("Name", "IsDeleted", Name = "idx_config_id_isdeleted")]
public partial class Config
{
    [Key]
    [Column("name", TypeName = "varchar(128)")]
    public string Name { get; set; } = null!;

    [Column("value", TypeName = "longtext")]
    public string? Value { get; set; }

    [Column("insDate", TypeName = "timestamp")]
    public byte[]? InsDate { get; set; }

    [Column("isDeleted", TypeName = "INTEGER(4)")]
    public long IsDeleted { get; set; }
}
