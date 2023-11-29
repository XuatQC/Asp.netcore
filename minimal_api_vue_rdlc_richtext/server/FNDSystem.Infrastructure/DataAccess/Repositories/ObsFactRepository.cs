using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;

namespace FNDSystem.Infrastructure.DataAccess.Repositories;
public class ObsFactRepository : GenericRepository<ObsFact>, IObsFactRepository
{
    public ObsFactRepository(DBMasterContext masterDBContext, DBSlaveContext slaveDBContext) : base(masterDBContext, slaveDBContext)
    {
    }

    public IEnumerable<VwObsFactAttach> GetVwObsFactAttaches(string num)
    {
        return slaveDBContext.VwObsFactAttach.Where(x => x.Num == num);
    }
}