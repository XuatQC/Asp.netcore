using Core.Entities;

namespace Infrastructure.DataAccess.Repositories;
public class AccountRepository : GenericRepository<Account>, IAccountRepository
{
    public AccountRepository(DBMasterContext masterDBContext, DBSlaveContext slaveDBContext) : base(masterDBContext, slaveDBContext)
    {
    }
    public Account Login(string email, string password)
    {
        var account = this.dbSetSlave.Where(acc => acc.Email == email).First();
        return account;

    }
}