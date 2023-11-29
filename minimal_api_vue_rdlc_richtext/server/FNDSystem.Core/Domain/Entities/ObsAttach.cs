using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FNDSystem.Core.Domain.Entities
{
    [Table("tbl_obs_attach")]
    [PrimaryKey(nameof(NumNoRevisions),nameof(SerialNum), nameof(FactNum))]
    public partial class ObsAttach
    {
        [Column("num_no_revisions")][MaxLength(50)] public string? NumNoRevisions { get; set; }
        [Column("serial_num")] public int SerialNum { get; set; }
        [Column("fact_num")] public int FactNum { get; set; }
        [Column("represent_photo_flag")][MaxLength(500)] public bool? RepresentPhotoFlag { get; set; }
        [Column("file_name")] public string? FileName { get; set; }
        [Column("size")][MaxLength(1000)] public string? Size { get; set; }
        [Column("delete_flag")][MaxLength(500)] public bool? DeleteFlag { get; set; }
        [Column("last_update")] public DateTime? LastUpdate { get; set; }
        [Column("last_user")] public string? LastUser { get; set; }
    }
}
