using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FNDSystem.Core.Domain.Entities
{
    [Table("vw_obs_fact_attach")]
    [Keyless]
    public class VwObsFactAttach
    {
        [Column("num")]
        public string Num { get; set; } = string.Empty;

        [Column("fact_num")]
        public int FactNum { get; set; } = 0;

        [Column("attach_fact_num")]
        public int? AttachFacNum { get; set; } = null;

        [Column("fact")]
        public string? Fact { get; set; } = null;

        [Column("fact_trans")]
        public string? FactTrans { get; set; } = null;

        [Column("comment")]
        public string? Comment { get; set; } = null;

        [Column("part_trans")]
        public bool PartTrans { get; set; } = false;

        [Column("plus_neutral_delta")]
        public int PlusNeutralDelta { get; set; } = 0;

        [Column("val_comp")]
        public bool ValComp { get; set; } = false;

        [Column("offer_field")]
        public string? OfferFields { get; set; } = null;

        [Column("poc")]
        public string? PoCs { get; set; } = null;

        [Column("safety_culture")]
        public string? SafetyCultures { get; set; } = null;

        [Column("display_order")]
        public int? DisplayOrder { get; set; } = null;

        [Column("last_user")]
        public string? LastUser { get; set; } = null;

        [Column("last_update")]
        public DateTime? LastUpdate { get; set; } = null;

        [Column("num_no_revisions")]
        public string? NumNoRevisions { get; set; } = null;

        [Column("serial_num")]
        public int? SerialNum { get; set; } = null;

        [Column("represent_photo_flag")]
        public bool? RepresentPhotoFlag { get; set; } = null;

        [Column("file_name")]
        public string? FileName { get; set; } = null;

        [Column("size")]
        public string? Size { get; set; } = null;

        [Column("delete_flag")]
        public bool? DeleteFlag { get; set; } = null;
    }
}
