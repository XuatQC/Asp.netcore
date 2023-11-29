namespace FNDSystem.Core.Dto
{
    public class FindWktTransHistoryRequest
    {
        public string? Item { get; set; }
        public int? Revisions { get; set; }
        public string? Translated { get; set; }
    }

    public class WktTransHistoryRequest
    {
        public string? TranItem { get; set; }
        public string? PlantName { get; set; }
        public string? Kinds { get; set; }
        public string? Fields { get; set; }
        public string? PartId { get; set; }
        public int? SerialNum { get; set; }
        public int? Revisions { get; set; }
        public string? EnglishEdition { get; set; }
        public string? DisplayLanguage { get; set; }
    }
}
