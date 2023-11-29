using FluentValidation;

namespace FNDSystem.Core.Dto
{
    public class ObsAttachResponseDto
    {
        public string? Num { get; set; }
        public int SerialNum { get; set; }
        public int SeqNum { get; set; }
        public bool RepresentPhot { get; set; }
        public string? FileName { get; set; }
        public int Size { get; set; }
        public bool? StateFlag { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
