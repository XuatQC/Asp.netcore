using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FNDSystem.Core.Dto
{
    [Keyless]
    public class PrintDataResponeseDto
    {
        [Column("num")] public string? Num { get; set; }
        [Column("plant_name")] public string? PlantName { get; set; }
        [Column("kinds")] public string? Kinds { get; set; }
        [Column("fields")] public string? Fields { get; set; }
        [Column("part_id")] public string? PartId { get; set; }
        [Column("serial_num")] public int? SerialNum { get; set; }
        [Column("revisions")] public int? Revisions { get; set; }
        [Column("title")] public string? Title { get; set; }
        [Column("title_trans")] public string? TitleTrans { get; set; }
        [Column("part_trans")] public bool? PartTrans { get; set; }
        [Column("observation_target")] public string? ObservationTarget { get; set; }
        [Column("final_ver")] public bool? FinalVer { get; set; }
        [Column("package_exclude")] public bool? PackageExclude { get; set; }
        [Column("val_comp")] public bool? ValComp { get; set; }
        [Column("general_comment")] public string? GeneralComment { get; set; }
        [Column("trans_range")] public string? TransRange { get; set; }
        [Column("trans_deadline")] public string? TransDeadline { get; set; }
        [Column("trans_lang")] public string? TransLang { get; set; }
        [Column("trans_state_req")] public bool? TransStateReq { get; set; }
        [Column("trans_state_req_id")] public string? TransStateReqId { get; set; }
        [Column("trans_req_date")] public DateTime? TransReqDate { get; set; }
        [Column("translating_state")] public bool? TranslatingState { get; set; }
        [Column("translating_state_id")] public string? TranslatingStateId { get; set; }
        [Column("translated_state")] public bool? TranslatedState { get; set; }
        [Column("translated_state_id")] public string? TranslatedStateId { get; set; }
        [Column("approval_state_pr")] public bool? ApprovalStatePr { get; set; }
        [Column("approval_state_pr_id")] public string? ApprovalStatePrId { get; set; }
        [Column("approval_state_pr_rev")] public int? ApprovalStatePrRev { get; set; }
        [Column("approval_state_pr_state")] public string? ApprovalStatePrState { get; set; }
        [Column("approval_state1")] public bool? ApprovalState1 { get; set; }
        [Column("approval_state1_id")] public string? ApprovalState1Id { get; set; }
        [Column("approval_state1_rev")] public int? ApprovalState1Rev { get; set; }
        [Column("approval_state2")] public bool? ApprovalState2 { get; set; }
        [Column("approval_state2_id")] public string? ApprovalState2Id { get; set; }
        [Column("approval_state2_rev")] public int? ApprovalState2Rev { get; set; }
        [Column("approval_state3")] public bool? ApprovalState3 { get; set; }
        [Column("approval_state3_id")] public string? ApprovalState3Id { get; set; }
        [Column("approval_state3_rev")] public int? ApprovalState3Rev { get; set; }
        [Column("approval_state_tl")] public bool? ApprovalStateTl { get; set; }
        [Column("approval_state_tl_id")] public string? ApprovalStateTlId { get; set; }
        [Column("approval_state_tl_rev")] public int? ApprovalStateTlRev { get; set; }
        [Column("approval_state_tl_state")] public string? ApprovalStateTlState { get; set; }
        [Column("language")] public string? Language { get; set; }
        [Column("hold")] public bool? Hold { get; set; }
        [Column("newest_flag")] public bool? NewestFlag { get; set; }
        [Column("delete_flag")] public bool? DeleteFlag { get; set; }
        [Column("editor")] public string? Editor { get; set; }
        [Column("last_update")] public DateTime? LastUpdate { get; set; }
        [Column("last_user")] public string? LastUser { get; set; }
        [Column("num_no_revisions")] public string? NumNoRevisions { get; set; }
        [Column("l_num")] public string? LNum { get; set; }
        [Column("field_order")] public int? FieldOrder { get; set; }
        [Column("trans_situation")] public string? TransSituation { get; set; }
        [Column("trans_cancel")] public string? TransCancel { get; set; }
        [Column("past_translated_rev")] public string? PastTranslatedRev { get; set; }
        [Column("past_translated_status")] public string? PastTranslatedStatus { get; set; }
        [Column("l_title")] public string? LTitle { get; set; }
        [Column("val_status")] public string? ValStatus { get; set; }
        [Column("l_trans_range")] public string? LTransRange { get; set; }
        [Column("tl_approval_status")] public string? TlApprovalStatus { get; set; }
    }
}
