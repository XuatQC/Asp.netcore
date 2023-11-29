using FNDSystem.Core.Domain;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.Dto;
using FNDSystem.Core.Helpers;
using Microsoft.EntityFrameworkCore;

namespace FNDSystem.Infrastructure.DataAccess.Repositories;
public class PoAndCRepository : GenericRepository<PoAndC>, IPoAndCRepository
{
    public PoAndCRepository(DBMasterContext masterDBContext, DBSlaveContext slaveDBContext) : base(masterDBContext, slaveDBContext)
    {
    }
}