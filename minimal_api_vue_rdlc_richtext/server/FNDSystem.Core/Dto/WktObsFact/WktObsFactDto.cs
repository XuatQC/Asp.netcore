using FluentValidation;

namespace FNDSystem.Core.Dto
{
    public class WktObsFactDto
    {
        public int Id { get; set; }
        public string? Num { get; set; }
        public string? SerialNum { get; set; }
        public string? Fact { get; set; }
        public string? FactTrans { get; set; }
        public string? Judge { get; set; }
        public string? PlusNeutralDelta { get; set; }
        public string? SeqNum { get; set; }
    }

    public class WktObsFactDtoValidator : AbstractValidator<WktObsFactDto>
    {
        public WktObsFactDtoValidator()
        {
            RuleFor(x => x.Num).Length(0, 58);
            RuleFor(x => x.SerialNum).Length(0, 120);
            RuleFor(x => x.PlusNeutralDelta).Length(0, 80);
            RuleFor(x => x.SeqNum).Length(0, 160);
        }
    }
}
