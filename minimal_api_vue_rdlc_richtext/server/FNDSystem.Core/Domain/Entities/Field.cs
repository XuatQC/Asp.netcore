using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FNDSystem.Core.Domain
{
    [Table("tbl_field")]
    [PrimaryKey(nameof(PlantName), nameof(Fields))]
    public partial class Field
    {
        [Column("plant_name")][MaxLength(10)] public string? PlantName { get; set; }
        [Column("fields")][MaxLength(4)] public string? Fields { get; set; }
        [Column("fields_name")][MaxLength(255)] public string? FieldsName { get; set; }
        [Column("fields_name_en")][MaxLength(255)] public string? FieldsNameEn { get; set; }
        [Column("dm_div")][MaxLength(1)] public string? DmDiv { get; set; }
        [Column("display_order")] public int? DisplayOrder { get; set; }
        [Column("output_order")] public int? OutputOrder { get; set; }
        [Column("last_update")] public DateTime? LastUpdate { get; set; }
        [Column("last_user")][MaxLength(3)] public string? LastUser { get; set; }
    }
}
