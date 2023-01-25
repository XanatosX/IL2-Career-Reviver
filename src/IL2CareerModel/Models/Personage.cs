using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IL2CarrerReviverModel.Models;

[Table("personage")]
[Index("PersonageId", Name = "idx_personageId")]
[Index("PersonageId", "Tvd", Name = "idx_personageId_tvd")]
[Index("PersonageId", "IsDeleted", "Tvd", Name = "idx_personageId_tvd_isdeleted")]
public partial class Personage
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("tvd", TypeName = "INTEGER(11)")]
    public long Tvd { get; set; }

    [Column("maxRank", TypeName = "INTEGER(4)")]
    public long MaxRank { get; set; }

    [Column("careers", TypeName = "INTEGER(11)")]
    public long Careers { get; set; }

    [Column("personageId", TypeName = "varchar(37)")]
    public string PersonageId { get; set; } = null!;

    [Column("insdate", TypeName = "timestamp")]
    public byte[]? Insdate { get; set; }

    [Column("isDeleted", TypeName = "INTEGER(4)")]
    public long IsDeleted { get; set; }
}
