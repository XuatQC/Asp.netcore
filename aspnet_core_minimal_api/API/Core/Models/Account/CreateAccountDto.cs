using FluentValidation;
using Core.Extensions;
using Core.Helpers.Enum;

namespace Core.Models
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
            RuleFor(x => x.Password).Length(8,20);
        }
    }
}