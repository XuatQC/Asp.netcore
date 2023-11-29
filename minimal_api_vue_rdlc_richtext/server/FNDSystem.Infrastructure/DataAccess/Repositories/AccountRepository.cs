using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;

namespace FNDSystem.Infrastructure.DataAccess.Repositories;
public class AccountRepository : GenericRepository<Account>, IAccountRepository
{
    public AccountRepository(DBMasterContext masterDBContext, DBSlaveContext slaveDBContext) : base(masterDBContext, slaveDBContext)
    {
    }

    /// <summary>
    /// ���O�C��
    /// </summary>
    /// <param name="email">���[��</param>
    /// <param name="password">�p�X���[�h</param>
    /// <returns>�A�J�E���g</returns>
    public Account Login(string email, string password)
    {
        var account = this.dbSetSlave.Where(acc => acc.Email == email).First();
        return account;

    }
}