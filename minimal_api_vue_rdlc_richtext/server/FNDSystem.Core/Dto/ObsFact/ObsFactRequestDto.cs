namespace FNDSystem.Core.Dto
{
    public class ObsFactRequestDto
    {
        public string? Num { get; set; } = string.Empty;
        public string? Fact { get; set; } = null;
        public string? FactTrans { get; set; } = null;
        public string? Comment { get; set; } = null;
        public bool? PartTrans { get; set; } = null;
        public int PlusNeutralDelta { get; set; } = 0;
        public bool ValComp { get; set; } = false;
        public int FactNum { get; set; } = 0;
        public string? OfferFields { get; set; } = null;
        public string? PoCs { get; set; } = null;
        public string? SafetyCultures { get; set; } = null;
        public int? DisplayOrder { get; set; } = null;
        public string LastUser { get; set; } = string.Empty;
        public DateTime? LastUpdate { get; set; } = null;
        public List<ObsAttachRequestDto>? ObsAttachs { get; set; } =  new List<ObsAttachRequestDto>();
    }
}
