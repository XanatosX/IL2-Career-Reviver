using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IL2CarrerReviverModel.Models;

[Table("ace")]
[Index("Id", "IsDeleted", Name = "idx_ace_id_isdeleted")]
public partial class Ace
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("careerId", TypeName = "INTEGER(11)")]
    public long CareerId { get; set; }

    [Column("name", TypeName = "varchar(45)")]
    public string Name { get; set; } = null!;

    [Column("deathDate", TypeName = "datetime")]
    public byte[] DeathDate { get; set; } = null!;

    [Column("insDate", TypeName = "timestamp")]
    public byte[]? InsDate { get; set; }

    [Column("isDeleted", TypeName = "INTEGER(4)")]
    public long IsDeleted { get; set; }
}
