using FluentValidation;

namespace FNDSystem.Core.Dto
{
    public class ObsAttachDto
    {
        public string? Num { get; set; }
        public int SerialNum { get; set; }
        public int SeqNum { get; set; }
        public bool RepresentPhot { get; set; }
        public string? FileName { get; set; }
        public string? Size { get; set; }
        public bool StateFlag { get; set; }
        public DateTime LastUpdate { get; set; }
    }
    public class ObsAttachDtoValidator : AbstractValidator<ObsAttachDto>
    {
        public ObsAttachDtoValidator()
        {
            RuleFor(x => x.Num).Length(0, 5000);
            RuleFor(x => x.Size).Length(0, 1000);
        }
    }
}
