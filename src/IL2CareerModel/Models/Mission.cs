using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IL2CarrerReviverModel.Models;

[Table("mission")]
[Index("SquadronId", "Date", "IsDeleted", Name = "idx_date_squadronid_isdeleted")]
[Index("CareerId", Name = "idx_mission_careerId")]
[Index("CareerId", "IsDeleted", Name = "idx_mission_careerId_isdeleted")]
[Index("Id", "IsDeleted", Name = "idx_mission_id_isdeleted")]
public partial class Mission
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("careerId", TypeName = "INTEGER(11)")]
    public long CareerId { get; set; }

    [Column("squadronId", TypeName = "INTEGER(11)")]
    public long SquadronId { get; set; }

    [Column("targetPoint", TypeName = "varchar(64)")]
    public string TargetPoint { get; set; } = null!;

    [Column("type", TypeName = "INTEGER(4)")]
    public long Type { get; set; }

    [Column("subtype", TypeName = "varchar(512)")]
    public string Subtype { get; set; } = null!;

    [Column("mTemplate", TypeName = "varchar(128)")]
    public string MTemplate { get; set; } = null!;

    [Column("date", TypeName = "datetime")]
    public byte[]? Date { get; set; }

    [Column("startTime", TypeName = "datetime")]
    public byte[]? StartTime { get; set; }

    [Column("endTime", TypeName = "datetime")]
    public byte[]? EndTime { get; set; }

    [Column("state", TypeName = "INTEGER(4)")]
    public long State { get; set; }

    [Column("route")]
    public string Route { get; set; } = null!;

    [Column("result")]
    public string Result { get; set; } = null!;

    [Column("mrange", TypeName = "float")]
    public double Mrange { get; set; }

    [Column("pilotsCount", TypeName = "INTEGER(11)")]
    public long PilotsCount { get; set; }

    [Column("maximumPlanes", TypeName = "INTEGER(11)")]
    public long MaximumPlanes { get; set; }

    [Column("pilot0", TypeName = "INTEGER(11)")]
    public long Pilot0 { get; set; }

    [Column("pilot1", TypeName = "INTEGER(11)")]
    public long Pilot1 { get; set; }

    [Column("pilot2", TypeName = "INTEGER(11)")]
    public long Pilot2 { get; set; }

    [Column("pilot3", TypeName = "INTEGER(11)")]
    public long Pilot3 { get; set; }

    [Column("pilot4", TypeName = "INTEGER(11)")]
    public long Pilot4 { get; set; }

    [Column("pilot5", TypeName = "INTEGER(11)")]
    public long Pilot5 { get; set; }

    [Column("pilot6", TypeName = "INTEGER(11)")]
    public long Pilot6 { get; set; }

    [Column("pilot7", TypeName = "INTEGER(11)")]
    public long Pilot7 { get; set; }

    [Column("pilot8", TypeName = "INTEGER(11)")]
    public long Pilot8 { get; set; }

    [Column("plane0", TypeName = "varchar(50)")]
    public string Plane0 { get; set; } = null!;

    [Column("plane1", TypeName = "varchar(50)")]
    public string Plane1 { get; set; } = null!;

    [Column("plane2", TypeName = "varchar(50)")]
    public string Plane2 { get; set; } = null!;

    [Column("plane3", TypeName = "varchar(50)")]
    public string Plane3 { get; set; } = null!;

    [Column("plane4", TypeName = "varchar(50)")]
    public string Plane4 { get; set; } = null!;

    [Column("plane5", TypeName = "varchar(50)")]
    public string Plane5 { get; set; } = null!;

    [Column("plane6", TypeName = "varchar(50)")]
    public string Plane6 { get; set; } = null!;

    [Column("plane7", TypeName = "varchar(50)")]
    public string Plane7 { get; set; } = null!;

    [Column("plane8", TypeName = "varchar(50)")]
    public string Plane8 { get; set; } = null!;

    [Column("ammo0", TypeName = "INTEGER(11)")]
    public long Ammo0 { get; set; }

    [Column("ammo1", TypeName = "INTEGER(11)")]
    public long Ammo1 { get; set; }

    [Column("ammo2", TypeName = "INTEGER(11)")]
    public long Ammo2 { get; set; }

    [Column("ammo3", TypeName = "INTEGER(11)")]
    public long Ammo3 { get; set; }

    [Column("ammo4", TypeName = "INTEGER(11)")]
    public long Ammo4 { get; set; }

    [Column("ammo5", TypeName = "INTEGER(11)")]
    public long Ammo5 { get; set; }

    [Column("ammo6", TypeName = "INTEGER(11)")]
    public long Ammo6 { get; set; }

    [Column("ammo7", TypeName = "INTEGER(11)")]
    public long Ammo7 { get; set; }

    [Column("ammo8", TypeName = "INTEGER(11)")]
    public long Ammo8 { get; set; }

    [Column("fuel0", TypeName = "float")]
    public double Fuel0 { get; set; }

    [Column("fuel1", TypeName = "float")]
    public double Fuel1 { get; set; }

    [Column("fuel2", TypeName = "float")]
    public double Fuel2 { get; set; }

    [Column("fuel3", TypeName = "float")]
    public double Fuel3 { get; set; }

    [Column("fuel4", TypeName = "float")]
    public double Fuel4 { get; set; }

    [Column("fuel5", TypeName = "float")]
    public double Fuel5 { get; set; }

    [Column("fuel6", TypeName = "float")]
    public double Fuel6 { get; set; }

    [Column("fuel7", TypeName = "float")]
    public double Fuel7 { get; set; }

    [Column("fuel8", TypeName = "float")]
    public double Fuel8 { get; set; }

    [Column("modificationsrequired0", TypeName = "INTEGER(11)")]
    public long Modificationsrequired0 { get; set; }

    [Column("modificationsrequired1", TypeName = "INTEGER(11)")]
    public long Modificationsrequired1 { get; set; }

    [Column("modificationsrequired2", TypeName = "INTEGER(11)")]
    public long Modificationsrequired2 { get; set; }

    [Column("modificationsrequired3", TypeName = "INTEGER(11)")]
    public long Modificationsrequired3 { get; set; }

    [Column("modificationsrequired4", TypeName = "INTEGER(11)")]
    public long Modificationsrequired4 { get; set; }

    [Column("modificationsrequired5", TypeName = "INTEGER(11)")]
    public long Modificationsrequired5 { get; set; }

    [Column("modificationsrequired6", TypeName = "INTEGER(11)")]
    public long Modificationsrequired6 { get; set; }

    [Column("modificationsrequired7", TypeName = "INTEGER(11)")]
    public long Modificationsrequired7 { get; set; }

    [Column("modificationsrequired8", TypeName = "INTEGER(11)")]
    public long Modificationsrequired8 { get; set; }

    [Column("modificationsdenied0", TypeName = "INTEGER(11)")]
    public long Modificationsdenied0 { get; set; }

    [Column("modificationsdenied1", TypeName = "INTEGER(11)")]
    public long Modificationsdenied1 { get; set; }

    [Column("modificationsdenied2", TypeName = "INTEGER(11)")]
    public long Modificationsdenied2 { get; set; }

    [Column("modificationsdenied3", TypeName = "INTEGER(11)")]
    public long Modificationsdenied3 { get; set; }

    [Column("modificationsdenied4", TypeName = "INTEGER(11)")]
    public long Modificationsdenied4 { get; set; }

    [Column("modificationsdenied5", TypeName = "INTEGER(11)")]
    public long Modificationsdenied5 { get; set; }

    [Column("modificationsdenied6", TypeName = "INTEGER(11)")]
    public long Modificationsdenied6 { get; set; }

    [Column("modificationsdenied7", TypeName = "INTEGER(11)")]
    public long Modificationsdenied7 { get; set; }

    [Column("modificationsdenied8", TypeName = "INTEGER(11)")]
    public long Modificationsdenied8 { get; set; }

    [Column("insDate", TypeName = "timestamp")]
    public byte[]? InsDate { get; set; }

    [Column("isDeleted", TypeName = "INTEGER(4)")]
    public long IsDeleted { get; set; }
}
