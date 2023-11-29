namespace FNDSystem.Core.Dto
{
    public class ObsRequestDto
    {
        public string Num { get; set; } = string.Empty;
        public string PlantName { get; set; } = string.Empty;
        public string Kinds { get; set; } = string.Empty;
        public string Fields { get; set; } = string.Empty;
        public string PartId { get; set; } = string.Empty;
        public int SerialNum { get; set; } = 0;
        public int Revisions { get; set; } = 0;
        public string NumNoRevisions { get; set; } = string.Empty;
        public string? Title { get; set; }
        public string? TitleTrans { get; set; }
        public bool? PartTrans { get; set; }
        public string? Scope { get; set; }
        public string? ScopeTrans { get; set; }
        public string? ScopeComment { get; set; }
        public bool? PartTransScope { get; set; }
        public string? ObservationTarget { get; set; }
        public bool? FinalVer { get; set; }
        public bool? PackageExclude { get; set; }
        public bool? ValComp { get; set; }
        public string? GeneralComment { get; set; }
        public int? TransRange { get; set; }
        public string? TransDeadline { get; set; }
        public string? TransLang { get; set; }
        public bool? TransStateReq { get; set; }
        public string? TransStateReqId { get; set; }
        public DateTime? TransReqDate { get; set; }
        public bool? TranslatingState { get; set; }
        public string? TranslatingStateId { get; set; }
        public bool? TranslatedState { get; set; }
        public string? TranslatedStateId { get; set; }
        public bool? ApprovalStatePr { get; set; }
        public string? ApprovalStatePrId { get; set; }
        public int? ApprovalStatePrRev { get; set; }
        public string? ApprovalStatePrState { get; set; }
        public bool? ApprovalState1 { get; set; }
        public string? ApprovalState1Id { get; set; }
        public int? ApprovalState1Rev { get; set; }
        public bool? ApprovalState2 { get; set; }
        public string? ApprovalState2Id { get; set; }
        public int? ApprovalState2Rev { get; set; }
        public bool? ApprovalState3 { get; set; }
        public string? ApprovalState3Id { get; set; }
        public int? ApprovalState3Rev { get; set; }
        public bool? ApprovalStateTl { get; set; }
        public string? ApprovalStateTlId { get; set; }
        public int? ApprovalStateTlRev { get; set; }
        public string? ApprovalStateTlState { get; set; }
        public string? Language { get; set; }
        public bool? Hold { get; set; }
        public bool? NewestFlag { get; set; }
        public bool DeleteFlag { get; set; }
        public string? Editor { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string? LastUser { get; set; }
        public string? LNum { get; set; }
        public int? FieldOrder { get; set; }
        public string? TransSituation { get; set; }
        public string? TransCancel { get; set; }
        public int? PastTranslatedRev { get; set; }
        public int? PastTranslatedStatus { get; set; }
    }
}
