﻿using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;

namespace FNDSystem.Infrastructure.DataAccess.Repositories;
public class ObsConclusionPastRepository : GenericRepository<ObsConclusionPast>, IObsConclusionPastRepository
{
    public ObsConclusionPastRepository(DBMasterContext masterDBContext, DBSlaveContext slaveDBContext) : base(masterDBContext, slaveDBContext)
    {
    }
}