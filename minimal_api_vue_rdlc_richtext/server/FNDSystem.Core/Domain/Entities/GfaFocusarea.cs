using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FNDSystem.Core.Domain.Entities
{
    [Table("tbl_gfa_focusarea")]
    public partial class GfaFocusarea
    {
        [Column("id")] public required int Id { get; set; }
        [Column("num")][MaxLength(50)] public string? Num { get; set; }
        [Column("serial_num")] public int SerialNum { get; set; }
        [Column("focus_area")] public string? FocusArea { get; set; }
        [Column("focus_area_trans")] public string? FocusAreaTrans { get; set; }
        [Column("judge")] public string? Judge { get; set; }
        [Column("part_trans")] public bool PartTrans { get; set; }
        [Column("last_update")] public DateTime LastUpdate { get; set; }
    }
}
