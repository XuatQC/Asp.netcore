using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FNDSystem.Core.Dto
{
    [Keyless]
    public class ObsExtendedReferObsFactDto
    {
        [Column("quantity")] public int Quantity { get; set; }
        [Column("revisions")] public string? Revisions { get; set; }
        [Column("seq_num")] public string? Fact { get; set; }
        [Column("fact")] public string? FactTrans { get; set; }
        [Column("fact_trans")] public string? SeqNum { get; set; }
    }
}
