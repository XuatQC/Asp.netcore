using FluentValidation;

namespace FNDSystem.Core.Dto
{
    public class UpdateObsDto
    {
        public string? PlantName { get; set; }
        public string? Kinds { get; set; }
        public string? Fields { get; set; }
        public string? PartId { get; set; }
        public int SerialNum { get; set; }
        public int Revisions { get; set; }
        public string? EnglishEdition { get; set; }
        public bool DeleteFlag { get; set; }
        public string? Title { get; set; }
        public string? TitleTrans { get; set; }
        public bool PartTrans { get; set; }
        public string? GeneralComment { get; set; }
        public bool FinalVer { get; set; }
        public bool PackageExclude { get; set; }
        public bool ValComp { get; set; }
        public string? ObservationTarget { get; set; }
        public bool ApprovalStatePr { get; set; }
        public bool ApprovalState1 { get; set; }
        public bool ApprovalState2 { get; set; }
        public bool ApprovalState3 { get; set; }
        public bool ApprovalStateTl { get; set; }
        public string? ApprovalStatePrId { get; set; }
        public string? ApprovalState1Id { get; set; }
        public string? ApprovalState2Id { get; set; }
        public string? ApprovalState3Id { get; set; }
        public string? ApprovalStateTlId { get; set; }
        public string? ApprovalStatePrState { get; set; }
        public string? ApprovalStateTlState { get; set; }
        public string? TransRange { get; set; }
        public string? TransDeadline { get; set; }
        public string? TransLang { get; set; }
        public DateTime TransReqDate { get; set; }
        public bool TransStateReq { get; set; }
        public string? TransStateReqId { get; set; }
        public bool TranslatedState { get; set; }
        public string? Editor { get; set; }
        public DateTime LastUpdate { get; set; }
        public string? LastUser { get; set; }
    }

    public class UpdateObsDtoValidator : AbstractValidator<UpdateObsDto>
    {
        public UpdateObsDtoValidator()
        {
            RuleFor(x => x.PlantName).Length(0, 10);
            RuleFor(x => x.Kinds).Length(0, 3);
            RuleFor(x => x.Fields).Length(0, 4);
            RuleFor(x => x.PartId).Length(0, 3);
            RuleFor(x => x.Title).Length(0, 1000);
            RuleFor(x => x.TitleTrans).Length(0, 1000);
            RuleFor(x => x.GeneralComment).Length(0, 1000);
            RuleFor(x => x.TransRange).Length(0, 10);
            RuleFor(x => x.TransRange).Length(0, 30);
            RuleFor(x => x.TransLang).Length(0, 1);
            RuleFor(x => x.TransStateReqId).Length(0, 3);
            RuleFor(x => x.ApprovalStatePrId).Length(0, 3);
            RuleFor(x => x.ApprovalStatePrState).Length(0, 10);
            RuleFor(x => x.ApprovalState1Id).Length(0, 3);
            RuleFor(x => x.ApprovalState2Id).Length(0, 3);
            RuleFor(x => x.ApprovalState3Id).Length(0, 3);
            RuleFor(x => x.ApprovalStateTlId).Length(0, 3);
            RuleFor(x => x.EnglishEdition).Length(0, 1);
            RuleFor(x => x.Editor).Length(0, 3);
            RuleFor(x => x.LastUser).Length(0, 3);
        }
    }
}
