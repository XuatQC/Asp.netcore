using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.ServicesContracts;

namespace FNDSystem.Core.Services;
public class WktAfiExampleService : GeneticService, IWktAfiExampleService
{
    private readonly IUnitOfWork unitOfWork;

    public WktAfiExampleService(IUnitOfWork unitOfWork)
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
        WktAfiExample wktAfiExample = new WktAfiExample() { Id = 0 };
        bool result = unitOfWork.WktAfiExampleRepository.Truncate(wktAfiExample);
        return result;
    }
    #endregion
}
