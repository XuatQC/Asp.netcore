using Core.Entities;

namespace Infrastructure.DataAccess.Repositories;
public class TenantRepository : GenericRepository<Tenant>, ITenantRepository
{
    public TenantRepository(DBMasterContext masterDBContext, DBSlaveContext slaveDBContext) : base(masterDBContext, slaveDBContext)
    {
    }
}