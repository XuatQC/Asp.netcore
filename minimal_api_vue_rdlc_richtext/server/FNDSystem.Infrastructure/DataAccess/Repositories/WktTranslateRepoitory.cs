using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace FNDSystem.Infrastructure.DataAccess.Repositories;
public class WktTranslateRepoitory : GenericRepository<WktTranslate>, IWktTranslateRepository
{
    public WktTranslateRepoitory(DBMasterContext masterDBContext, DBSlaveContext slaveDBContext) : base(masterDBContext, slaveDBContext)
    {
        
    }

    #region wkt-Translateからすべてのレコードを削除します
    /// <summary>
    /// wkt-Translateからすべてのレコードを削除します
    /// </summary>
    public void DeleteAll()
    {
        this.slaveDBContext?.Database.ExecuteSqlRaw("Delete from wkt_translate");
    }
    #endregion
}