using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Infrastructure;

namespace FNDSystem.Infrastructure.DataAccess.Repositories;
public class TenantRepository : GenericRepository<Tenant>, ITenantRepository
{
    public TenantRepository(DBMasterContext masterDBContext, DBSlaveContext slaveDBContext) : base(masterDBContext, slaveDBContext)
    {
    }
}