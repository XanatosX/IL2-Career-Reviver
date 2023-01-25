using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IL2CarrerReviverModel.Models;

[Table("log")]
public partial class Log
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("profileId", TypeName = "varchar(45)")]
    public string ProfileId { get; set; } = null!;

    [Column("careerId", TypeName = "INTEGER(11)")]
    public long CareerId { get; set; }

    [Column("action", TypeName = "varchar(45)")]
    public string? Action { get; set; }

    [Column("msg", TypeName = "varchar(512)")]
    public string Msg { get; set; } = null!;

    [Column("request")]
    public string? Request { get; set; }

    [Column("insDate", TypeName = "timestamp")]
    public byte[]? InsDate { get; set; }

    [Column("level", TypeName = "INTEGER(4)")]
    public long Level { get; set; }
}
