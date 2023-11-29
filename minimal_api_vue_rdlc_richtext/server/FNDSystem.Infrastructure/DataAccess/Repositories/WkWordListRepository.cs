using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace FNDSystem.Infrastructure.DataAccess.Repositories;
public class WkWordListRepository : GenericRepository<WkWordList>, IWkWordListRepository
{
    public WkWordListRepository(DBMasterContext masterDBContext, DBSlaveContext slaveDBContext) : base(masterDBContext, slaveDBContext)
    {

    }

    #region SortDataList
    /// <summary>
    /// 一覧並び替え
    /// </summary>
    /// <returns></returns>
    public IEnumerable<WkWordList>? SortDataList()
    {
        var obsList = this.slaveDBContext?.WkWordLists?
                          .FromSql($@"Call SP_Sort_wk_WordList ();")
                          .ToList();

        return obsList;
    }
    #endregion
}