using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IL2CarrerReviverModel.Models;

[Table("award")]
[Index("CareerId", Name = "idx_award_careerId")]
[Index("CareerId", "IsDeleted", Name = "idx_award_careerId_isdeleted")]
[Index("Id", "IsDeleted", Name = "idx_award_id_isdeleted")]
[Index("IsDeleted", "PersonageId", Name = "idx_award_personageId_isdeleted")]
[Index("PersonageAwardId", Name = "idx_personageAwardId")]
[Index("PilotId", "IsDeleted", Name = "idx_pilotid_isdeleted")]
public partial class Award
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("careerId", TypeName = "INTEGER(11)")]
    public long CareerId { get; set; }

    [Column("type", TypeName = "INTEGER(11)")]
    public long Type { get; set; }

    [Column("date", TypeName = "datetime")]
    public byte[]? Date { get; set; }

    [Column("pilotId", TypeName = "INTEGER(11)")]
    public long PilotId { get; set; }

    [Column("pilotName", TypeName = "varchar(64)")]
    public string? PilotName { get; set; }

    [Column("pilotRank", TypeName = "INTEGER(4)")]
    public long PilotRank { get; set; }

    [Column("squadName", TypeName = "varchar(45)")]
    public string? SquadName { get; set; }

    [Column("insDate", TypeName = "timestamp")]
    public byte[]? InsDate { get; set; }

    [Column("isDeleted", TypeName = "INTEGER(4)")]
    public long IsDeleted { get; set; }

    [Column(TypeName = "varchar(64)")]
    public string PersonageId { get; set; } = null!;

    [Column("x", TypeName = "varchar(50)")]
    public string X { get; set; } = null!;

    [Column("y", TypeName = "varchar(50)")]
    public string Y { get; set; } = null!;

    [Column(TypeName = "INTEGER(10)")]
    public long CausedByType { get; set; }

    [Column(TypeName = "varchar(64)")]
    public string? CausedById { get; set; }

    [Column(TypeName = "INTEGER(4)")]
    public long Show { get; set; }

    [Column(TypeName = "INTEGER(11)")]
    public long SquadId { get; set; }

    [Column("squadConfigId", TypeName = "INTEGER(11)")]
    public long SquadConfigId { get; set; }

    [Column(TypeName = "datetime")]
    public byte[]? GameTime { get; set; }

    [Column(TypeName = "varchar(64)")]
    public string PersonageAwardId { get; set; } = null!;
}
