using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNDSystem.Core.Domain.Entities
{
    [Table("tbl_pd_attach")]
    public partial class PdAttach
    {
        [Column("id")] public required int Id { get; set; }
        [Column("num")][MaxLength(50)] public string? Num { get; set; }
        [Column("serial_num")] public int SerialNum { get; set; }
        [Column("perspective")] public string? Perspective { get; set; }
        [Column("perspective_trans")] public string? PerspectiveTrans { get; set; }
        [Column("judge")] public string? Judge { get; set; }
        [Column("part_trans")] public bool PartTrans { get; set; }
        [Column("last_update")] public DateTime LastUpdate { get; set; }
    }
}
