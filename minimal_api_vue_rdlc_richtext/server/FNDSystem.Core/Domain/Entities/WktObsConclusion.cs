using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FNDSystem.Core.Domain.Entities
{
    [Table("wkt_obs_conclusion")]
    public class WktObsConclusion
    {
        [Column("id")] public int Id { get; set; }
        [Column("num")][MaxLength(50)] public string? Num { get; set; }
        [Column("serial_num")] public int? SerialNum { get; set; }
        [Column("conclusion")] public string? Conclusion { get; set; }
        [Column("conclusion_trans")] public string? ConclusionTrans { get; set; }
        [Column("comment")] public string? Comment { get; set; }
        [Column("connect_fact")][MaxLength(120)] public string? ConnectFact { get; set; }
    }
}
