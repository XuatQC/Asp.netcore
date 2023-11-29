using System.Linq.Expressions;
using Core.Entities;

namespace Infrastructure.DataAccess.Repositories;
public interface IAccountRepository : IGenericRepository<Account>
{
    Account Login(string email, string password);
}