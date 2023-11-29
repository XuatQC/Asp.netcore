namespace FNDSystem.Core.Dto
{
    public class FindObsRequestDto
    {
        public string? Num { get; set; }
        public string? LNum { get; set; }
        public int? SerialNum { get; set; }
        public string? PlantName { get; set; }
        public string? Kinds { get; set; }
        public string? Fields { get; set; }
        public string? PartId { get; set; }
        public int? Revisions { get; set; }
        public string? Language { get; set; } //英語版 (Lang)
        public string? LastUser { get; set; }
        public string? FreeWord { get; set; }
        public bool? ChkTl { get; set; }
        public string? DmDivision { get; set; } // g_strDM区分
        public string? NumNoRevisions { get; set; }
    }
}
