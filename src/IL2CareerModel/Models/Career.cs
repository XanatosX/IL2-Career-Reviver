using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IL2CarrerReviverModel.Models;

[Table("career")]
[Index("Id", "IsDeleted", Name = "idx_career_id_isdeleted")]
[Index("PersonageId", "IsDeleted", Name = "idx_career_personageId_isDeleted")]
[Index("PlayerId", "IsDeleted", Name = "idx_playerId_isDeleted")]
public partial class Career
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("personageId", TypeName = "varchar(37)")]
    public string PersonageId { get; set; } = null!;

    [Column("playerId", TypeName = "INTEGER(11)")]
    public Pilot? Player { get; set; }

    [Column("tvd", TypeName = "INTEGER(11)")]
    public long Tvd { get; set; }

    [Column("currentDate", TypeName = "datetime")]
    public byte[]? CurrentDate { get; set; }

    public Squadron? Squadron { get; set; }

    [Column("state", TypeName = "INTEGER(11)")]
    public long State { get; set; }

    [Column("insDate", TypeName = "timestamp")]
    public byte[]? InsDate { get; set; }

    [Column("isDeleted", TypeName = "INTEGER(4)")]
    public long IsDeleted { get; set; }

    [Column("transferInfo", TypeName = "varchar(512)")]
    public string TransferInfo { get; set; } = null!;

    [Column("startDate", TypeName = "datetime")]
    public byte[]? StartDate { get; set; }

    [Column("uiData", TypeName = "varchar(512)")]
    public string UiData { get; set; } = null!;

    [Column("infoId", TypeName = "varchar(32)")]
    public string InfoId { get; set; } = null!;

    [Column("phaseId", TypeName = "varchar(32)")]
    public string PhaseId { get; set; } = null!;

    [Column("neverBeCommander", TypeName = "INTEGER(4)")]
    public long NeverBeCommander { get; set; }

    [Column("extends", TypeName = "INTEGER(11)")]
    public long Extends { get; set; }

    [Column("ironMan", TypeName = "INTEGER(4)")]
    public long IronMan { get; set; }

    [Column("cuid", TypeName = "varchar(37)")]
    public string Cuid { get; set; } = null!;
}
