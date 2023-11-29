using FluentValidation;
using FNDSystem.Core.Dto;

namespace FNDSystem.Core.Dto
{
    public class LoginRequestDto
    {
        public required string Initial { get; set; }
        public required string PlantName { get; set; }
        public required string Pass { get; set; }
    }

    public class LoginResponseDto : UserDto
    {
        public string? FieldError { get; set; }
        public string? Token { get; set; }
    }

    public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginRequestDtoValidator()
        {
            RuleFor(x => x.Initial).Length(1,3).WithMessage("Login must be 3 characters in length, but not \'ZZZ\'. (Use ESC to clear the login box)");
            RuleFor(x => x.Pass).Length(1, 255);
        }
    }
}