using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FNDSystem.Core.Domain.Entities
{

    [Table("tbl_peerreview")]
    [PrimaryKey(nameof(Years), nameof(HoldNum), nameof(ReviewName), nameof(PlantName))]
    public partial class PeerReview
    {
        [Column("years")] public int Years { get; set; }
        [Column("hold_num")] public int HoldNum { get; set; }
        [Column("review _name")][MaxLength(50)] public string ReviewName { get; set; } = string.Empty;
        [Column("plant_name")][MaxLength(50)] public string PlantName { get; set; } = string.Empty;
        [Column("hold")] public bool Hold { get; set; }
        [Column("past_pr_subject")] public bool PastPrSubject { get; set; }
        [Column("dm_division")][MaxLength(1)] public string? DmDivision { get; set; }
        [Column("offer_division")][MaxLength(1)] public string? OfferDivision { get; set; }
        [Column("sync_subject_live")] public bool SyncSubjectLive { get; set; }
        [Column("sync_subject_past")] public bool SyncSubjectPast { get; set; }
        [Column("follow_up")][MaxLength(50)] public string? FollowUp { get; set; }
        [Column("last_update")] public DateTime? LastUpdate { get; set; }
        [Column("last_user")][MaxLength(3)] public string? LastUser { get; set; }

        public ReviewMember? ReviewMember { get; set; }

    }
}
