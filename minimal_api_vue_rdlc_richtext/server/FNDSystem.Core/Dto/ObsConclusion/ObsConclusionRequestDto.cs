namespace FNDSystem.Core.Dto
{
    public class ObsConclusionRequestDto
    {
        public int ConclusionNum { get; set; } = 0;
        public string Num { get; set; } = string.Empty;
        public string? Conclusion { get; set; } = null;
        public string? ConclusionTrans { get; set; } = null;
        public string? Comment { get; set; } = null;
        public bool PartTrans { get; set; } = false;
        public string? ConnectFact { get; set; } = null;
        public int? DisplayOrder { get; set; } = null;
        public string LastUser { get; set; } = string.Empty;
        public DateTime? LastUpdate { get; set; } = null;
    }
}
