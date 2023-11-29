using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FNDSystem.Core.Domain
{
    [Table("tbl_safety_culture")]
    [PrimaryKey(nameof(PlantName), nameof(Code), nameof(SerialNum))]
    public partial class SafetyCulture
    {
        [Column("plant_name")][MaxLength(10)] public string? PlantName { get; set; }
        [Column("safety_culture")][MaxLength(255)] public string? Code { get; set; }
        [Column("serial_num")] public int? SerialNum { get; set; }
        [Column("safety_culture_name")][MaxLength(255)] public string? Name { get; set; }
        [Column("safety_culture_name_en")][MaxLength(255)] public string? NameEn { get; set; }
        [Column("achievement_goal")][MaxLength(255)] public string? AchievementGoal { get; set; }
        [Column("achievement_goal_en")][MaxLength(255)] public string? AchievementGoalEn { get; set; }
        [Column("display_order")] public int? DisplayOrder { get; set; }
        [Column("last_update")] public DateTime? LastUpdate { get; set; }
        [Column("last_user")][MaxLength(3)] public string? LastUser { get; set; }
    }
}
