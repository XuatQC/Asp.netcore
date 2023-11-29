using System.Linq.Expressions;
using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;

namespace FNDSystem.Core.Domain.RepositoryContracts;
public interface IAccountRepository : IGenericRepository<Account>
{
    Account Login(string email, string password);
}