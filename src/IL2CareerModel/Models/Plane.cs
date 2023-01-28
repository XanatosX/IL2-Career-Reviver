using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IL2CareerModel.Models;

[Table("plane")]
[Index("Id", "IsDeleted", Name = "idx_plane_id_isdeleted")]
[Index("SquadronId", "IsDeleted", Name = "idx_plane_squadronid_isdeleted")]
public partial class Plane
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("squadronId", TypeName = "INTEGER(11)")]
    public long SquadronId { get; set; }

    [Column("config", TypeName = "varchar(256)")]
    public string Config { get; set; } = null!;

    [Column("state", TypeName = "INTEGER(11)")]
    public long State { get; set; }

    [Column("stateDate", TypeName = "datetime")]
    public byte[]? StateDate { get; set; }

    [Column("stateEndDate", TypeName = "datetime")]
    public byte[]? StateEndDate { get; set; }

    [Column("insDate", TypeName = "timestamp")]
    public byte[]? InsDate { get; set; }

    [Column("isDeleted", TypeName = "INTEGER(4)")]
    public long IsDeleted { get; set; }
}
