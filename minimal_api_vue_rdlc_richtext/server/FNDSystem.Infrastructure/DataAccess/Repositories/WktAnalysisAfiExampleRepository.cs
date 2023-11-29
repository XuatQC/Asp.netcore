using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace FNDSystem.Infrastructure.DataAccess.Repositories;
public class WktAnalysisAfiExampleRepository : GenericRepository<WktAnalysisAfiExample>, IWktAnalysisAfiExampleRepository
{
    public WktAnalysisAfiExampleRepository(DBMasterContext masterDBContext, DBSlaveContext slaveDBContext) : base(masterDBContext, slaveDBContext)
    {

    }
}