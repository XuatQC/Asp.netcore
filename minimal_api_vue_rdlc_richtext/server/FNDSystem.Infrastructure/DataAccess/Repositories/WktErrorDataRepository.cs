using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace FNDSystem.Infrastructure.DataAccess.Repositories;
public class WktErrorDataRepository : GenericRepository<WktErrorData>, IWktErrorDataRepository
{
    public WktErrorDataRepository(DBMasterContext masterDBContext, DBSlaveContext slaveDBContext) : base(masterDBContext, slaveDBContext)
    {

    }
}