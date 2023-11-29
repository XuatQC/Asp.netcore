using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
namespace FNDSystem.Infrastructure.DataAccess.Repositories;
public class NumberControlRepository : GenericRepository<NumberControl>, INumberControlRepository
{
    public NumberControlRepository(DBMasterContext masterDBContext, DBSlaveContext slaveDBContext) : base(masterDBContext, slaveDBContext)
    {
    }
}
