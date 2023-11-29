namespace FNDSystem.Core.Dto
{
    public class WktTranslateDto
    {
        public int? ID { get; set; }
        public string? PlantName { get; set; }
        public string? Kinds { get; set; }
        public string? Fields { get; set; }
        public string? PartId { get; set; }
        public int? BranchNum { get; set; }
        public int? Revisions { get; set; }
        public string? Num { get; set; }
        public string? EnglishEdition { get; set; }
        public string? OfferNum { get; set; }
        public string? Title { get; set; }
        public string? TitleTrans { get; set; }
        public int? VerTransDone { get; set; }
        public int? VerOriginal { get; set; }
        public string? Editor { get; set; }
        public string? TransSituation { get; set; }
        public string? TransArrival { get; set; }
        public string? TransCancel { get; set; }
        public string? TransUrgent { get; set; }
        public string? ContactMemo { get; set; }
        public DateTime? OutputDate { get; set; }
        public DateTime? CaptureDate { get; set; }
        public DateTime? TransDate { get; set; }
        public string? Translator { get; set; }
        public string? CancelStatus { get; set; }
        public string? SelectionCancelStatus { get; set; }
        public string? TransScope { get; set; }
        public string? TransDeadline { get; set; }
        public string? TransLang { get; set; }
        public DateTime? TransReqDate { get; set; }
        public string? FileName { get; set; }
        public bool? SelectCheck { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string? LastUser { get; set; }
    }
}
