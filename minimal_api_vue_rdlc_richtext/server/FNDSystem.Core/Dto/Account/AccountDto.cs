using FNDSystem.Core.Extensions;
using FluentValidation;
using FNDSystem.Core.Helpers;

namespace FNDSystem.Core.Dto
{
    public class AccountDto
    {
        public int? Id { get; set; }
        public string? TenantCode { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? RoleKbn { get; set; }
    }

    public class AccountDtoValidator : AbstractValidator<AccountDto>
    {
        public AccountDtoValidator()
        {
            RuleFor(x => x.Code).Length(10, 15);
            RuleFor(x => x.TenantCode).Length(10, 10);
            RuleFor(x => x.Name).Length(1, 80);
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.RoleKbn).WithInValues(RoleKbn.Values);
        }
    }
}