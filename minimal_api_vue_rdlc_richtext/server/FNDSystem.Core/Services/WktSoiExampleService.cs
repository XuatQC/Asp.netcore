using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.ServicesContracts;

namespace FNDSystem.Core.Services;
public class WktSoiExampleService : GeneticService, IWktSoiExampleService
{
    private readonly IUnitOfWork unitOfWork;

    public WktSoiExampleService(IUnitOfWork unitOfWork)
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
        WktSoiExample wktSoiExample = new WktSoiExample() { Id = 0 };
        bool result = unitOfWork.WktSoiExampleRepository.Truncate(wktSoiExample);
        return result;
    }
    #endregion
}
