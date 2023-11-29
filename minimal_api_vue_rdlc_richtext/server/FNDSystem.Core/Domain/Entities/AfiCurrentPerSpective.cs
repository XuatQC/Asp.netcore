using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FNDSystem.Core.Domain.Entities
{
    [Table("tbl_afi_currentperspective")]
    public partial class AfiCurrentPerSpective
    {
        [Column("id")] public required int Id { get; set; }
        [Column("num")][MaxLength(50)] public string? Num { get; set; }
        [Column("serial_num")] public int SerialNum { get; set; }
        [Column("current")] public string? Current { get; set; }
        [Column("current_trans")] public string? CurrentTrans { get; set; }
        [Column("judge")] public string? Judge { get; set; }
        [Column("part_trans")] public bool PartTrans { get; set; }
        [Column("list_update")] public DateTime ListUpdate { get; set; }

    }
}
