using FluentValidation;

namespace FNDSystem.Core.Dto
{
    public class ObsFactDto
    {
        public int? FactNum { get; set; }
        public string? Num { get; set; }
        public string? Fact { get; set; }
        public string? FactTrans { get; set; }
        public string? Comment { get; set; }
        public bool PartTrans { get; set; }
        public int PlusNeutralDelta { get; set; }
        public bool ValComp { get; set; }
        public string? OferField { get; set; }
        public string? Poc { get; set; }
        public string? SafetyCulture { get; set; }
        public DateTime? LastUpdate { get; set; }
    }

    public class ObsFactDtoValidator : AbstractValidator<ObsFactDto>
    {
        public ObsFactDtoValidator()
        {
            RuleFor(x => x.Num).Length(0, 58);
        }
    }
}
