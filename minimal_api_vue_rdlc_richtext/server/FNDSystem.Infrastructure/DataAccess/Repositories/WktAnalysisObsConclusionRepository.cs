using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace FNDSystem.Infrastructure.DataAccess.Repositories;
public class WktAnalysisObsConclusionRepository : GenericRepository<WktAnalysisObsConclusion>, IWktAnalysisObsConclusionRepository
{
    public WktAnalysisObsConclusionRepository(DBMasterContext masterDBContext, DBSlaveContext slaveDBContext) : base(masterDBContext, slaveDBContext)
    {

    }
}