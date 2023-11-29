using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace FNDSystem.Infrastructure.DataAccess.Repositories;
public class WktStrExamplesRepository : GenericRepository<WktStrExamples>, IWktStrExamplesRepository
{
    public WktStrExamplesRepository(DBMasterContext masterDBContext, DBSlaveContext slaveDBContext) : base(masterDBContext, slaveDBContext)
    {

    }
}