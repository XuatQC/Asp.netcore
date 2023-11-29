using System;

namespace Infrastructure.DataAccess.Repositories;

public interface IUnitOfWork
{
    ITenantRepository TenantRepository { get; }

    IAccountRepository AccountRepository { get; }
    void Commit();
    void Rollback();
    Task CommitAsync();
    Task RollbackAsync();
}
