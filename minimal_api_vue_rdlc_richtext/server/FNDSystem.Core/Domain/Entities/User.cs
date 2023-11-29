using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FNDSystem.Core.Domain.Entities
{

    [Table("tbl_user")]
    [PrimaryKey(nameof(Initial))]
    public partial class User
    {
        [Column("initial")][MaxLength(3)] public string Initial { get; set; } = string.Empty;
        [Column("name")][MaxLength(50)] public string Name { get; set; } = string.Empty;
        [Column("roma_name")][MaxLength(50)] public string? RomaName { get; set; }
        [Column("respns_area")][MaxLength(4)] public string? RespnsArea { get; set; }
        [Column("comment")][MaxLength(255)] public string? Comment { get; set; }
        [Column("language")][MaxLength(1)] public string? Language { get; set; }
        [Column("coordinator")] public bool Coordinator { get; set; }
        [Column("trans")] public bool Trans { get; set; }
        [Column("valid")] public bool Valid { get; set; }
        [Column("pass")][MaxLength(255)] public string? Pass { get; set; }
        [Column("last_update")] public DateTime? LastUpdate { get; set; }
        [Column("last_user")][MaxLength(3)] public string? LastUser { get; set; }
    }
}
