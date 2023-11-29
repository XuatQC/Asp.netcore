using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace FNDSystem.Infrastructure.DataAccess.Repositories;
public class WktAnalysisGfaExampleRepository : GenericRepository<WktAnalysisGfaExamples>, IWktAnalysisGfaExampleRepository
{
    public WktAnalysisGfaExampleRepository(DBMasterContext masterDBContext, DBSlaveContext slaveDBContext) : base(masterDBContext, slaveDBContext)
    {

    }
}