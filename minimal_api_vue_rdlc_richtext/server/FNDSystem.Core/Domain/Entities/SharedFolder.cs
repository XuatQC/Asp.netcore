using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FNDSystem.Core.Domain.Entities
{

    [Table("tbl_sharedfolder")]
    [PrimaryKey(nameof(PlantName), nameof(DispName))]
    public partial class SharedFolder
    {
        [Column("plant_name")]
        [MaxLength(50)]
        public string PlantName { get; set; } = string.Empty;

        [Column("menu_pposi")]
        public int? MenuPposi { get; set; } = null;

        [Column("disp_name")]
        [MaxLength(40)]
        public string DispName { get; set; } = string.Empty;

        [Column("disp_name_en")]
        [MaxLength(40)]
        public string? DispNameEn { get; set; } = null;

        [Column("path")]
        [MaxLength(255)]
        public string? Path { get; set; } = string.Empty;

        [Column("valid")]
        public bool Valid { get; set; } = false;

        [Column("valid_en")]
        public bool ValidEn { get; set; } = false;

        [Column("last_update")]
        public DateTime? LastUpdate { get; set; } = null;

        [Column("last_user")]
        [MaxLength(3)]
        public string? LastUser { get; set; } = null;
    }
}
