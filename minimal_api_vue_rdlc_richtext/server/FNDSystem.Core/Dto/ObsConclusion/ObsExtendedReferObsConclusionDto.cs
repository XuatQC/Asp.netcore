using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FNDSystem.Core.Dto
{
    [Keyless]
    public class ObsExtendedReferObsConclusionDto
    {
        [Column("quantity")] public int Quantity { get; set; }
        [Column("revisions")] public string? Revisions { get; set; }
        [Column("conclusion")] public string? Conclusion { get; set; }
        [Column("conclusion_trans")] public string? ConclusionTrans { get; set; }
        [Column("seq_num")] public string? SeqNum { get; set; }
    }
}
