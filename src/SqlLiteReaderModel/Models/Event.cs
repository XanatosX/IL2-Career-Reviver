using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IL2CarrerReviverModel.Models;

[Table("event")]
[Index("Date", "Type", "PilotId", "IsDeleted", Name = "idx_calendar")]
[Index("CareerId", Name = "idx_event_careerId")]
[Index("CareerId", "IsDeleted", Name = "idx_event_careerId_isdeleted")]
[Index("Id", "IsDeleted", Name = "idx_event_id_isdeleted")]
[Index("PilotId", "Date", "Type", "IsDeleted", Name = "idx_pilotid_date_type_isdeleted")]
public partial class Event
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("date", TypeName = "datetime")]
    public byte[]? Date { get; set; }

    [Column("type", TypeName = "INTEGER(11)")]
    public long Type { get; set; }

    [Column("pilotId", TypeName = "INTEGER(11)")]
    public long PilotId { get; set; }

    [Column("rankId", TypeName = "INTEGER(11)")]
    public long RankId { get; set; }

    [Column("missionId", TypeName = "INTEGER(11)")]
    public long MissionId { get; set; }

    [Column("squadronId", TypeName = "INTEGER(11)")]
    public long SquadronId { get; set; }

    [Column("careerId", TypeName = "INTEGER(11)")]
    public long CareerId { get; set; }

    [Column("ipar1", TypeName = "INTEGER(11)")]
    public long Ipar1 { get; set; }

    [Column("ipar2", TypeName = "INTEGER(11)")]
    public long Ipar2 { get; set; }

    [Column("ipar3", TypeName = "INTEGER(11)")]
    public long Ipar3 { get; set; }

    [Column("ipar4", TypeName = "INTEGER(11)")]
    public long Ipar4 { get; set; }

    [Column("tpar1", TypeName = "varchar(255)")]
    public string Tpar1 { get; set; } = null!;

    [Column("tpar2", TypeName = "varchar(255)")]
    public string Tpar2 { get; set; } = null!;

    [Column("tpar3", TypeName = "varchar(255)")]
    public string Tpar3 { get; set; } = null!;

    [Column("tpar4", TypeName = "varchar(255)")]
    public string Tpar4 { get; set; } = null!;

    [Column("insdate", TypeName = "timestamp")]
    public byte[]? Insdate { get; set; }

    [Column("isDeleted", TypeName = "INTEGER(4)")]
    public long IsDeleted { get; set; }
}
