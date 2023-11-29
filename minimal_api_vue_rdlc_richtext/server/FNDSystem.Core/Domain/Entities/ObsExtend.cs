using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FNDSystem.Core.Domain.Entities
{
    [Table("vw_obs_extend")]
    [Keyless]
    public class ObsExtend
    {
        [Column("num")]
        [MaxLength(50)]
        public string Num { get; set; } = string.Empty;

        [Column("title")]
        [MaxLength(1000)]
        public string? Title { get; set; } = null;

        [Column("title_trans")]
        [MaxLength(1000)]
        public string? TitleTrans { get; set; } = null;

        [Column("part_trans")]
        public bool? PartTrans { get; set; } = false;

        [Column("scope")]
        public string? Scope { get; set; } = null;

        [Column("scope_trans")]
        public string? ScopeTrans { get; set; } = null;

        [Column("scope_comment")]
        public string? ScopeComment { get; set; } = null;

        [Column("part_trans_scope")]
        public bool? PartTransScope { get; set; } = false;

        [Column("observation_target")]
        [MaxLength(2)]
        public string? ObservationTarget { get; set; } = null;

        [Column("final_ver")]
        public bool? FinalVer { get; set; } = false;

        [Column("package_exclude")]
        public bool? PackageExclude { get; set; } = false;

        [Column("val_comp")]
        public bool? ValComp { get; set; } = false;

        [Column("general_comment")]
        [MaxLength(1000)]
        public string? GeneralComment { get; set; } = null;

        [Column("trans_range")]
        public int? TransRange { get; set; } = 2;

        [Column("trans_deadline")]
        [MaxLength(30)]
        public string? TransDeadline { get; set; } = null;

        [Column("trans_lang")]
        [MaxLength(1)]
        public string? TransLang { get; set; } = null;

        [Column("trans_state_req")]
        public bool? TransStateReq { get; set; } = false;

        [Column("trans_state_req_id")]
        [MaxLength(3)]
        public string? TransStateReqId { get; set; } = null;

        [Column("trans_req_date")]
        public DateTime? TransReqDate { get; set; } = null;

        [Column("translating_state")]
        public bool? TranslatingState { get; set; } = false;

        [Column("translating_state_id")]
        [MaxLength(3)]
        public string? TranslatingStateId { get; set; } = null;

        [Column("translated_state")]
        public bool? TranslatedState { get; set; } = false;

        [Column("translated_state_id")]
        [MaxLength(3)]
        public string? TranslatedStateId { get; set; } = null;

        [Column("approval_state_pr")]
        public bool? ApprovalStatePr { get; set; } = false;

        [Column("approval_state_pr_id")]
        [MaxLength(3)]
        public string? ApprovalStatePrId { get; set; } = null;

        [Column("approval_state_pr_rev")]
        public int? ApprovalStatePrRev { get; set; } = null;

        [Column("approval_state_pr_state")]
        [MaxLength(10)]
        public string? ApprovalStatePrState { get; set; } = null;

        [Column("approval_state1")]
        public bool? ApprovalState1 { get; set; } = true;

        [Column("approval_state1_id")]
        [MaxLength(3)]
        public string? ApprovalState1Id { get; set; } = null;

        [Column("approval_state1_rev")]
        public int? ApprovalState1Rev { get; set; } = null;

        [Column("approval_state2")]
        public bool? ApprovalState2 { get; set; } = false;

        [Column("approval_state2_id")]
        [MaxLength(3)]
        public string? ApprovalState2Id { get; set; } = null;

        [Column("approval_state2_rev")]
        public int? ApprovalState2Rev { get; set; } = null;

        [Column("approval_state3")]
        public bool? ApprovalState3 { get; set; } = false;

        [Column("approval_state3_id")]
        [MaxLength(3)]
        public string? ApprovalState3Id { get; set; } = null;

        [Column("approval_state3_rev")]
        public int? ApprovalState3Rev { get; set; } = null;

        [Column("approval_state_tl")]
        public bool? ApprovalStateTl { get; set; } = false;

        [Column("approval_state_tl_id")]
        [MaxLength(3)]
        public string? ApprovalStateTlId { get; set; } = null;

        [Column("approval_state_tl_rev")]
        public int? ApprovalStateTlRev { get; set; } = null;

        [Column("approval_state_tl_state")]
        [MaxLength(25)]
        public string? ApprovalStateTlState { get; set; } = null;

        [Column("hold")]
        public bool? Hold { get; set; } = true;

        [Column("delete_flag")]
        public bool DeleteFlag { get; set; } = false;

        [Column("editor")]
        [MaxLength(3)]
        public string? Editor { get; set; } = null;

        [Column("last_update")]
        public DateTime? LastUpdate { get; set; } = null;

        [Column("last_user")]
        [MaxLength(3)]
        public string? LastUser { get; set; } = null;

        [Column("plant_name")]
        public string PlantName { get; set; } = string.Empty;

        [Column("kinds")]
        public string Kinds { get; set; } = string.Empty;

        [Column("fields")]
        public string Fields { get; set; } = string.Empty;

        [Column("part_id")]
        public string PartId { get; set; } = string.Empty;

        [Column("serial_num")]
        public int SerialNum { get; set; } = 0;

        [Column("revisions")]
        public int Revisions { get; set; } = 0;

        [Column("language")]
        public string? Language { get; set; } = null;

        [Column("num_no_revisions")]
        public string? NumNoRevisions { get; set; } = null;

        [Column("newest_flag")]
        public bool? NewestFlag { get; set; } = null;

        [Column("l_num")]
        public string? LNum { get; set; } = null;

        [Column("field_order")]
        public int? FieldOrder { get; set; } = null;

        [Column("trans_situation")]
        public string? TransSituation { get; set; } = null;

        [Column("trans_cancel")]
        public string? TransCancel { get; set; } = null;

        [Column("past_translated_rev")]
        public int? PastTranslatedRev { get; set; } = null;

        [Column("past_translated_status")]
        public int? PastTranslatedStatus { get; set; } = null;
    }
}
