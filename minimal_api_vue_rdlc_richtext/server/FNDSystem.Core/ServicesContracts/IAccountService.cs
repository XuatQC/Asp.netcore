using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;

namespace FNDSystem.Core.ServicesContracts;

public interface IAccountService
{
    IEnumerable<Account>? FindMany(FindAccountRequestDto accountDto);
    Account Create(CreateAccountRequestDto accountDto);
}
