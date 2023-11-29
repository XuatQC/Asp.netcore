using FluentValidation;

namespace FNDSystem.Core.Dto
{
    public class UserDto
    {
        public int Id { get; set; } = 0;
        public string Initial { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string? RomaName { get; set; }
        public string? RespnsArea { get; set; }
        public string? Comment { get; set; }
        public string? Language { get; set; }
        public bool Coordinator { get; set; }
        public bool Trans { get; set; }
        public bool Valid { get; set; }
        public string? Pass { get; set; }
    }

    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.Initial).Length(0, 3);
            RuleFor(x => x.Name).Length(0, 50);
            RuleFor(x => x.RomaName).Length(0, 50);
            RuleFor(x => x.RespnsArea).Length(0, 4);
            RuleFor(x => x.Comment).Length(0, 255);
            RuleFor(x => x.Language).Length(0, 1);
            RuleFor(x => x.Pass).Length(0, 255);
        }
    }
}
