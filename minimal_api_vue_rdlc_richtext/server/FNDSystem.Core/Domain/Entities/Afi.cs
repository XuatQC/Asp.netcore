using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FNDSystem.Core.Domain.Entities
{
    [Table("tbl_afi")]
    public partial class Afi
    {
        [Column("id")] public required int Id { get; set; }
        [Column("plant_name")][MaxLength(10)] public string? PlantName { get; set; }
        [Column("years")] public int Years { get; set; }
        [Column("kinds")][MaxLength(3)] public string? Kinds { get; set; }
        [Column("fields")][MaxLength(4)] public string? Fields { get; set; }
        [Column("part_id")][MaxLength(3)] public string? PartId { get; set; }
        [Column("serial_num")] public int SerialNum { get; set; }
        [Column("revisions")] public int Revisions { get; set; }
        [Column("achievement_goal")][MaxLength(10)] public string? AchievementGoal { get; set; }
        [Column("title")][MaxLength(1000)] public string? Title { get; set; }
        [Column("title_trans")][MaxLength(1000)] public string? TitleTrans { get; set; }
        [Column("part_trans")] public bool PartTrans { get; set; }
        [Column("observation_target")][MaxLength(2)] public string? ObservationTarget { get; set; }
        [Column("final_ver")] public bool FinalVer { get; set; }
        [Column("package_exclude")] public bool PackageExclude { get; set; }
        [Column("val_comp")] public bool ValComp { get; set; }
        [Column("general_jdc")][MaxLength(1000)] public string? GeneralJdc { get; set; }
        [Column("connect_num")][MaxLength(10)] public string? ConnectNum { get; set; }
        [Column("connect_memo")][MaxLength(100)] public string? ConnectMemo { get; set; }
        [Column("connect_memo_trans")][MaxLength(60)] public string? ConnectMemoTrans { get; set; }
        [Column("connect_memo_parttrans")] public bool ConnectMemoParttrans { get; set; }
        [Column("trans_range")][MaxLength(10)] public string? TransRange { get; set; }
        [Column("trans_deadline")][MaxLength(30)] public string? TransDeadline { get; set; }
        [Column("trans_lang")][MaxLength(1)] public string? TransLang { get; set; }
        [Column("trans_state_req")] public bool TransStateReq { get; set; }
        [Column("trans_state_req_id")][MaxLength(3)] public string? TransStateReqId { get; set; }
        [Column("trans_req_date")] public DateTime TransReqDate { get; set; }
        [Column("translating_state")] public bool TranslatingState { get; set; }
        [Column("translating_state_id")][MaxLength(3)] public string? TranslatingStateId { get; set; }
        [Column("translated_state")] public bool TranslatedState { get; set; }
        [Column("translated_state_id")][MaxLength(3)] public string? TranslatedStateId { get; set; }
        [Column("approval_state_pr")] public bool ApprovalStatePr { get; set; }
        [Column("approval_state_pr_id")][MaxLength(3)] public string? ApprovalStatePrId { get; set; }
        [Column("approval_state_pr_rev")] public int ApprovalStatePrRev { get; set; }
        [Column("approval_state_pr_state")][MaxLength(10)] public string? ApprovalStatePrState { get; set; }
        [Column("approval_state1")] public bool ApprovalState1 { get; set; }
        [Column("approval_state1_id")][MaxLength(3)] public string? ApprovalState1Id { get; set; }
        [Column("approval_state1_rev")] public int ApprovalState1Rev { get; set; }
        [Column("approval_state2")] public bool ApprovalState2 { get; set; }
        [Column("approval_state2_id")][MaxLength(3)] public string? ApprovalState2Id { get; set; }
        [Column("approval_state2_rev")] public int ApprovalState2Rev { get; set; }
        [Column("approval_state3")] public bool ApprovalState3 { get; set; }
        [Column("approval_state3_id")][MaxLength(3)] public string? ApprovalState3Id { get; set; }
        [Column("approval_state3_rev")] public int ApprovalState3Rev { get; set; }
        [Column("approval_state_tl")] public bool ApprovalStateTl { get; set; }
        [Column("approval_state_tl_id")][MaxLength(3)] public string? ApprovalStateTlId { get; set; }
        [Column("approval_state_tl_rev")] public int ApprovalStateTlRev { get; set; }
        [Column("approval_state_tl_state")][MaxLength(25)] public string? ApprovalStateTlState { get; set; }
        [Column("english_edition")][MaxLength(1)] public string? EnglishEdition { get; set; }
        [Column("hold")] public bool Hold { get; set; }
        [Column("newest_flag")] public bool NewestFlag { get; set; }
        [Column("delete_flag")] public bool DeleteFlag { get; set; }
        [Column("editor")][MaxLength(3)] public string? Editor { get; set; }
        [Column("last_update")] public DateTime LastUpdate { get; set; }
        [Column("last_user")][MaxLength(3)] public string? LastUser { get; set; }

    }
}