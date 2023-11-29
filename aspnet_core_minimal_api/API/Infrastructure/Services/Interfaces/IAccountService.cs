using Core.Entities;
using Core.Models;

namespace Infrastructure.Services;

public interface IAccountService
{
    IEnumerable<Account> FindMany(FindAccountRequestDto? accountDto);
    Account Create(CreateAccountRequestDto accountDto);
}
