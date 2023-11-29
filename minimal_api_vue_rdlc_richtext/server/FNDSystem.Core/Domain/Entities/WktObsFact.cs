using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FNDSystem.Core.Domain.Entities
{
    [Table("wkt_obs_fact")]
    public class WktObsFact
    {
        [Column("id")] public int Id { get; set; }
        [Column("num")][MaxLength(50)] public string? Num { get; set; }
        [Column("serial_num")] public int? SerialNum { get; set; }
        [Column("fact")] public string? Fact { get; set; }
        [Column("fact_trans")] public string? FactTrans { get; set; }
        [Column("comment")] public string? Comment { get; set; }
        [Column("plus_neutral_delta")] public int? PlusNeutralDelta { get; set; }
        [Column("seq_num")] public int? SeqNum { get; set; }
    }
}
