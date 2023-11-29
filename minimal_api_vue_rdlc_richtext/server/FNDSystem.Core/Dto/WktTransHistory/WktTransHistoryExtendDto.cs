using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FNDSystem.Core.Dto
{
    [Keyless]
    public class WktTransHistoryExtendDto
    {
        [Column("revisions")] public int? Revisions { get; set; }
        [Column("serial_num")] public string? SerialNum { get; set; }
        [Column("item_name_jp")] public string? ItemNameJp { get; set; }
        [Column("item_name_en")] public string? ItemNameEn { get; set; }
        [Column("part_trans")] public string? PartTrans { get; set; }
        [Column("translated_state")] public bool TranslatedState { get; set; }
        [Column("trans_range")] public string? TransRange { get; set; }
        [Column("num")] public string? Num { get; set; }

    }
}
