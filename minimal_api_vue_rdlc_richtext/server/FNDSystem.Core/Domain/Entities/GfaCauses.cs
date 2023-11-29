using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FNDSystem.Core.Domain.Entities
{
    [Table("tbl_gfa_causes")]
    public partial class GfaCauses
    {
        [Column("id")] public required int Id { get; set; }
        [Column("num")][MaxLength(5000)] public string? Num { get; set; }
        [Column("serial_num")][MaxLength(500)] public string? SerialNum { get; set; }
        [Column("cause")] public string? Cause { get; set; }
        [Column("cause_trans")] public string? CauseTrans { get; set; }
        [Column("judge")] public string? Judge { get; set; }
        [Column("part_trans")] public bool PartTrans { get; set; }
        [Column("last_update")] public DateTime LastUpdate { get; set; }
    }
}
