using Bogus;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FNDSystem.Core.Domain.Entities
{

    [Table("tbl_reviewmember")]
    [PrimaryKey(nameof(PlantName), nameof(Initial))]
    public partial class ReviewMember
    {
        [Column("plant_name")][MaxLength(50)] public string? PlantName { get; set; }
        [Column("initial")][MaxLength(3)] public string? Initial { get; set; }
        [Column("tl")] public bool Tl { get; set; }
        [Column("judge")] public bool Judge { get; set; }
        [Column("editor")] public bool Editor { get; set; }
        [Column("trans")] public bool Trans { get; set; }
        [Column("judge1")][MaxLength(3)] public string? Judge1 { get; set; }
        [Column("judge2")][MaxLength(3)] public string? Judge2 { get; set; }
        [Column("judge3")][MaxLength(3)] public string? Judge3 { get; set; }
        [Column("tl_judge")][MaxLength(3)] public string? TlJudge { get; set; }
        [Column("comment")][MaxLength(255)] public string? Comment { get; set; }
        [Column("last_update")] public DateTime? LastUpdate { get; set; }
        [Column("last_user")][MaxLength(3)] public string? LastUser { get; set; }

        public PeerReview? PeerReview { get; set; }

    }
}
