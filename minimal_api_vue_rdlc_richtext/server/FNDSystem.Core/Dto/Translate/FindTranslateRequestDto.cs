namespace FNDSystem.Core.Dto
{
    public class FindTranslateRequestDto
    {
        public string? Num { get; set; }
        public string? PlantName { get; set; }
        public string? LNum { get; set; }
        public int? OfferNum { get; set; }
        public int? BranchOrSerialNum { get; set; }
        public string? Kinds { get; set; }
        public string? Fields { get; set; }
        public string? PartId { get; set; }
        public int? Revisions { get; set; }
        public string? TransLang { get; set; } //英語版 (Lang)
        public string? Initial { get; set; }
    }
}
