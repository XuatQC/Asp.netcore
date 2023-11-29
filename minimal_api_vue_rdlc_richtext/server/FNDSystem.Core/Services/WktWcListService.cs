using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.ServicesContracts;

namespace FNDSystem.Core.Services;
public class WktWcListService : GeneticService, IWktWcListService
{
    private readonly IUnitOfWork unitOfWork;

    public WktWcListService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    #region データ削除
    /// <summary>
    /// データ削除
    /// </summary>
    /// <returns>true: 成功します。 false: 失敗します</returns>
    public bool RemoveAll()
    {
        WktWcList wktWcList = new WktWcList() { Id = 0 };
        bool result = unitOfWork.WktWcListRepository.Truncate(wktWcList);
        return result;
    }
    #endregion
}
