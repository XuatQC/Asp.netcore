using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FNDSystem.Core.Domain.Entities
{
    [Table("tbl_obs_conclusion")]
    [PrimaryKey(nameof(Num),nameof(ConclusionNum))]
    public partial class ObsConclusion
    {
        [Column("num")]
        [MaxLength(50)]
        public string Num { get; set; } = string.Empty;

        [Column("conclusion_num")]
        public int ConclusionNum { get; set; } = 1;

        [Column("conclusion")]
        public string? Conclusion { get; set; } = null;

        [Column("conclusion_trans")]
        public string? ConclusionTrans { get; set; } = null;

        [Column("comment")]
        public string? Comment { get; set; } = null;

        [Column("part_trans")]
        public bool? PartTrans { get; set; } = false;

        [Column("connect_fact")]
        [MaxLength(240)]
        public string? ConnectFact { get; set; } = null;

        [Column("display_order")]
        public int? DisplayOrder { get; set; }

        [Column("last_user")]
        [MaxLength(3)]
        public string? LastUser { get; set; } = null;

        [Column("last_update")]
        public DateTime? LastUpdate { get; set; } = null;
    }
}
