using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.ServicesContracts;

namespace FNDSystem.Core.Services;
public class WktAnalysisObsConclusionService : GeneticService, IWktAnalysisObsConclusionService
{
    private readonly IUnitOfWork unitOfWork;

    public WktAnalysisObsConclusionService(IUnitOfWork unitOfWork)
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
        WktAnalysisObsConclusion wktAnalysisGfaExample = new WktAnalysisObsConclusion() { Id = 0 };
        bool result = unitOfWork.WktAnalysisObsConclusionRepository.Truncate(wktAnalysisGfaExample);
        return result;
    }
    #endregion
}
