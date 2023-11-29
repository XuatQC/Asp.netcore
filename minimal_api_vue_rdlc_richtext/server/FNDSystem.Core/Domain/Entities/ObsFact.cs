using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FNDSystem.Core.Domain.Entities
{

    [Table("tbl_obs_fact")]
    [PrimaryKey(nameof(Num),nameof(FactNum))]
    public partial class ObsFact
    {
        [Column("num")]
        [MaxLength(50)]
        public string Num { get; set; } = string.Empty;

        [Column("fact_num")]
        public int FactNum { get; set; } = 1;

        [Column("fact")]
        public string? Fact { get; set; } = null;

        [Column("fact_trans")]
        public string? FactTrans { get; set; } = null;

        [Column("comment")]
        public string? Comment { get; set; } = null;

        [Column("part_trans")]
        public bool? PartTrans { get; set; } = false;

        [Column("plus_neutral_delta")]
        public int? PlusNeutralDelta { get; set; } = 3;

        [Column("val_comp")]
        public bool? ValComp { get; set; } = false;

        [Column("offer_field")]
        [MaxLength(255)]
        public string? OfferFields { get; set; } = null;

        [Column("poc")]
        [MaxLength(255)]
        public string? PoCs { get; set; } = null;

        [Column("safety_culture")]
        [MaxLength(255)]
        public string? SafetyCultures { get; set; } = null;

        [Column("display_order")]
        public int? DisplayOrder { get; set; }

        [Column("last_user")]
        [MaxLength(3)]
        public string? LastUser { get; set; } = null;

        [Column("last_update")]
        public DateTime? LastUpdate { get; set; } = null;
    }
}
