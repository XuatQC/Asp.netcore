namespace FNDSystem.Core.Dto
{
    public class FindPrintRequestDto
    {
        public string? PlantName { get; set; }
        public string? Fields { get; set; }
        public string? PartId { get; set; }
        public string? EnglishEdition { get; set; } //英語版 (Lang)
        public string? FreeWord { get; set; }
        public bool? ChkTl { get; set; }
        public string? SheetName { get; set; }
        public string? CallForm { get; set; }
    }
}
