using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace FNDSystem.Infrastructure.DataAccess.Repositories;
public class WktGfaExamplesRepository : GenericRepository<WktGfaExamples>, IWktGfaExamplesRepository
{
    public WktGfaExamplesRepository(DBMasterContext masterDBContext, DBSlaveContext slaveDBContext) : base(masterDBContext, slaveDBContext)
    {

    }
}