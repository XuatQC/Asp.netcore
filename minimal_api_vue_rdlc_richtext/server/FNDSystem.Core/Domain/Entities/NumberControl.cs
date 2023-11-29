using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FNDSystem.Core.Domain.Entities
{
    [Table("tbl_number_control")]
    [PrimaryKey(nameof(PlantName), nameof(Kinds), nameof(Fields), nameof(PartId), nameof(SerialNum), nameof(Revisions))]
    public class NumberControl
    {
        [Column("num")]
        [MaxLength(50)]
        public string Num { get; set; } = string.Empty;

        [Column("plant_name")]
        [MaxLength(10)]
        public string PlantName { get; set; } = string.Empty;

        [Column("kinds")]
        [MaxLength(3)]
        public string Kinds { get; set; } = string.Empty;

        [Column("fields")]
        [MaxLength(4)]
        public string Fields { get; set; } = string.Empty;

        [Column("part_id")]
        [MaxLength(3)]
        public string PartId { get; set; } = string.Empty;

        [Column("serial_num")]
        public int SerialNum { get; set; } = 0;

        [Column("revisions")]
        public int Revisions { get; set; } = 0;

        [Column("num_no_revisions")]
        public string NumNoRevisions { get; set; } = string.Empty;

        [Column("language")]
        [MaxLength(1)]
        public string? Language { get; set; } = null;

        [Column("delete_flag")]
        public bool DeleteFlag { get; set; } = false;

        [Column("newest_flag")]
        public bool NewestFlag { get; set; } = false;

        [Column("last_update")]
        public DateTime? LastUpdate { get; set; }

        [Column("last_user")]
        [MaxLength(3)]
        public string? LastUser { get; set; } = string.Empty;
    }
}
