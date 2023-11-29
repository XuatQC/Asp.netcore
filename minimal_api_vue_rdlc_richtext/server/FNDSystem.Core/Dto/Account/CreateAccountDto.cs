using FluentValidation;

namespace FNDSystem.Core.Dto
{
    public class CreateAccountRequestDto : AccountDto
    {
        public string? Password { get; set; }
    }

    public class CreateAccountRequestDtoValidator : AbstractValidator<CreateAccountRequestDto>
    {
        public CreateAccountRequestDtoValidator()
        {
            Include(new AccountDtoValidator());
            RuleFor(x => x.Password).Length(8, 20);
        }
    }
}