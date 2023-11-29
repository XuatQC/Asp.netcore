using FNDSystem.Core.Domain;
using FNDSystem.Core.Domain.RepositoryContracts;

namespace FNDSystem.Infrastructure.DataAccess.Repositories;
public class ActivityRepository : GenericRepository<Activity>, IActivityRepository
{
    public ActivityRepository(DBMasterContext masterDBContext, DBSlaveContext slaveDBContext) : base(masterDBContext, slaveDBContext)
    {
    }

}