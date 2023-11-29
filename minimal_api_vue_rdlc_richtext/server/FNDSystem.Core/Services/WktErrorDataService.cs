using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.ServicesContracts;

namespace FNDSystem.Core.Services;
public class WktErrorDataService : GeneticService, IWktErrorDataService
{
    private readonly IUnitOfWork unitOfWork;

    public WktErrorDataService(IUnitOfWork unitOfWork)
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
        WktErrorData wktErrorData = new WktErrorData() { Id = 0 };
        bool result = unitOfWork.WktErrorDataRepository.Truncate(wktErrorData);
        return result;
    }
    #endregion
}
