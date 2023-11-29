namespace FNDSystem.Core.Dto
{
    public class ObsAttachRequestDto
    {
        public string? NumNoRevisions { get; set; }
        public int? SerialNum { get; set; }
        public byte[]? Src { get; set; }
        public string? Extension { get; set; }
        public int FactNum { get; set; }
        public bool? RepresentPhotoFlag { get; set; }
        public string? FileName { get; set; }
        public string? Size { get; set; }
        public bool? DeleteFlag { get; set; }
        public DateTime? LastUpdate { get; set; } = DateTime.Now;
        public string? LastUser { get; set; }
            
    }
}
