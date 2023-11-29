using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FNDSystem.Core.Domain.Entities
{
    [Table("tbl_obs_fact_past")]
    [Keyless]
    public class ObsFactPast
    {
        [Column("num")][MaxLength(50)] public string Num { get; set; } = string.Empty;
        [Column("fact_num")] public int FactNum { get; set; }
        [Column("fact")] public string? Fact { get; set; }
        [Column("fact_trans")] public string? FactTrans { get; set; }
        [Column("comment")] public string? Comment { get; set; }
        [Column("part_trans")] public int PartTrans { get; set; }
        [Column("plus_neutral_delta")] public int PlusNeutralDelta { get; set; }
        [Column("offer_field")][MaxLength(255)] public string? OfferFields { get; set; } = null;
        [Column("poc")][MaxLength(255)] public string? PoCs { get; set; } = null;
        [Column("safety_culture")][MaxLength(255)] public string? SafetyCultures { get; set; } = null;
        [Column("display_order")] public int? DisplayOrder { get; set; } = 1;
        [Column("last_user")][MaxLength(3)] public string? LastUser { get; set; } = null;
        [Column("last_update")] public DateTime? LastUpdate { get; set; } = null;
    }
}
