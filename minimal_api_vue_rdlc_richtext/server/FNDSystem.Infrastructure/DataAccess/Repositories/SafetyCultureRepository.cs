using FNDSystem.Core.Domain;
using FNDSystem.Core.Domain.RepositoryContracts;

namespace FNDSystem.Infrastructure.DataAccess.Repositories;
public class SafetyCultureRepository : GenericRepository<SafetyCulture>, ISafetyCultureRepository
{
    public SafetyCultureRepository(DBMasterContext masterDBContext, DBSlaveContext slaveDBContext) : base(masterDBContext, slaveDBContext)
    {
    }
}