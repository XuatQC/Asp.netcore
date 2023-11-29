using FluentValidation;

namespace FNDSystem.Core.Dto
{
    public class WktObsConclusionDto
    {
        public int Id { get; set; }
        public string? Num { get; set; }
        public string? SerialNum { get; set; }
        public string? Conclusion { get; set; }
        public string? ConclusionTrans { get; set; }
        public string? Judge { get; set; }
        public string? PartTrans { get; set; }
    }

    public class WktObsConclusionDtoValidator : AbstractValidator<WktObsConclusionDto>
    {
        public WktObsConclusionDtoValidator()
        {
            RuleFor(x => x.Num).Length(0, 58);
            RuleFor(x => x.SerialNum).Length(0, 120);
            RuleFor(x => x.PartTrans).Length(0, 120);
        }
    }
}
