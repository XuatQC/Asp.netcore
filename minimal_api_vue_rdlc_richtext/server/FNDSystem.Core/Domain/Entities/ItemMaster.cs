using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FNDSystem.Core.Domain.Entities
{
    [Table("tbl_itemmaster")]
    [PrimaryKey(nameof(PlantName), nameof(Item), nameof(Code))]
    public partial class ItemMaster
    {
        [Column("plant_name")][MaxLength(10)] public string? PlantName { get; set; }
        [Column("item")][MaxLength(50)] public string? Item { get; set; }
        [Column("code")][MaxLength(50)] public string? Code { get; set; }
        [Column("name")][MaxLength(50)] public string? Name { get; set; }
        [Column("name_en")][MaxLength(50)] public string? NameEn { get; set; }
        [Column("division")][MaxLength(10)] public string? Division { get; set; }
        [Column("last_update")] public DateTime? LastUpdate { get; set; }
        [Column("last_user")][MaxLength(3)] public string? LastUser { get; set; }
    }
}
