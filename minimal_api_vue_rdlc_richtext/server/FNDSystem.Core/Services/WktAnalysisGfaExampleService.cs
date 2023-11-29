using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.ServicesContracts;

namespace FNDSystem.Core.Services;
public class WktAnalysisGfaExampleService : GeneticService, IWktAnalysisGfaExampleService
{
    private readonly IUnitOfWork unitOfWork;

    public WktAnalysisGfaExampleService(IUnitOfWork unitOfWork)
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
        WktAnalysisGfaExamples wktAnalysisGfaExample = new WktAnalysisGfaExamples() { Id = 0 };
        bool result = unitOfWork.WktAnalysisGfaExampleRepository.Truncate(wktAnalysisGfaExample);
        return result;
    }
    #endregion
}
