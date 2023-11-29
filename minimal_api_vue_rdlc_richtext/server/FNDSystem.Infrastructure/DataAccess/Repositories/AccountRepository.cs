using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;

namespace FNDSystem.Infrastructure.DataAccess.Repositories;
public class AccountRepository : GenericRepository<Account>, IAccountRepository
{
    public AccountRepository(DBMasterContext masterDBContext, DBSlaveContext slaveDBContext) : base(masterDBContext, slaveDBContext)
    {
    }

    /// <summary>
    /// ログイン
    /// </summary>
    /// <param name="email">メール</param>
    /// <param name="password">パスワード</param>
    /// <returns>アカウント</returns>
    public Account Login(string email, string password)
    {
        var account = this.dbSetSlave.Where(acc => acc.Email == email).First();
        return account;

    }
}