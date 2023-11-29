using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FNDSystem.Core.Domain
{
    [Table("tbl_po&c")]
    [PrimaryKey(nameof(PlantName), nameof(Fields), nameof(PoC), nameof(SerialNum))]
    public partial class PoAndC
    {
        [Column("plant_name")][MaxLength(10)] public string PlantName { get; set; } = string.Empty;
        [Column("fields")][MaxLength(255)] public string Fields { get; set; } = string.Empty;
        [Column("po_c")][MaxLength(255)] public string PoC { get; set; } = string.Empty;
        [Column("serial_num")] public int SerialNum { get; set; }
        [Column("po_c_name")][MaxLength(255)] public string? PoCName { get; set; }
        [Column("po_c_name_en")][MaxLength(255)] public string? PoCNameEn { get; set; }
        [Column("achievement_goal")][MaxLength(255)] public string? AchievementGoal { get; set; }
        [Column("achievement_goal_en")][MaxLength(255)] public string? AchievementGoalEn { get; set; }
        [Column("display_order")] public int? DisplayOrder { get; set; }
        [Column("last_update")] public DateTime? LastUpdate { get; set; }
        [Column("last_user")][MaxLength(3)] public string? LastUser { get; set; }
    }
}
