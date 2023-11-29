using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FNDSystem.Core.Domain
{
    [Table("tbl_activity")]
    [PrimaryKey(nameof(PlantName), nameof(Target))]
    public partial class Activity
    {
        [Column("plant_name")][MaxLength(10)] public string? PlantName { get; set; }
        [Column("target")][MaxLength(10)] public string? Target { get; set; }
        [Column("target_name")][MaxLength(50)] public string? TargetName { get; set; }
        [Column("target_name_en")][MaxLength(50)] public string? TargetNameEn { get; set; }
        [Column("display_order")] public int? DisplayOrder { get; set; }
        [Column("last_update")] public DateTime? LastUpdate { get; set; }
        [Column("last_user")][MaxLength(3)] public string? LastUser { get; set; }
    }
}
