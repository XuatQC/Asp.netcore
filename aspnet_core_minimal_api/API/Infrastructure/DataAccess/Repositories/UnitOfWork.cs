namespace Infrastructure.DataAccess.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private DBMasterContext masterDBContext;
    private DBSlaveContext slaveDBContext;

    private ITenantRepository? tenantRepository;
    private IAccountRepository? accountRepository;

    public UnitOfWork(DBMasterContext masterDBContext, DBSlaveContext slaveDBContext)
    {
        this.masterDBContext = masterDBContext;
        this.slaveDBContext = slaveDBContext;
    }

    public ITenantRepository TenantRepository
    {
        get { return tenantRepository = tenantRepository ?? new TenantRepository(masterDBContext, slaveDBContext); }
    }

    public IAccountRepository AccountRepository
    {
        get { return accountRepository = accountRepository ?? new AccountRepository(masterDBContext, slaveDBContext); }
    }

    public void Save()
    {
        this.masterDBContext.SaveChanges();
    }

    public void Commit()
            => this.masterDBContext.SaveChanges();


    public async Task CommitAsync()
        => await this.masterDBContext.SaveChangesAsync();


    public void Rollback()
        => this.masterDBContext.Dispose();


    public async Task RollbackAsync()
        => await this.masterDBContext.DisposeAsync();
}
