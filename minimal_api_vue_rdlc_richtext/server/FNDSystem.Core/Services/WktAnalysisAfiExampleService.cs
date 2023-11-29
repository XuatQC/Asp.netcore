using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.ServicesContracts;

namespace FNDSystem.Core.Services;
public class WktAnalysisAfiExampleService : GeneticService, IWktAnalysisAfiExampleService
{
    private readonly IUnitOfWork unitOfWork;

    public WktAnalysisAfiExampleService(IUnitOfWork unitOfWork)
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
        WktAnalysisAfiExample wktAnalysisAfiExample = new WktAnalysisAfiExample() { Id = 0 };
        bool result = unitOfWork.WktAnalysisAfiExampleRepository.Truncate(wktAnalysisAfiExample);
        return result;
    }
    #endregion
}
