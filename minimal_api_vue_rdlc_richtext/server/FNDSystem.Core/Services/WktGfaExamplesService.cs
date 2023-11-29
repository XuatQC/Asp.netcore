using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.ServicesContracts;

namespace FNDSystem.Core.Services;
public class WktGfaExamplesService : GeneticService, IWktGfaExamplesService
{
    private readonly IUnitOfWork unitOfWork;

    public WktGfaExamplesService(IUnitOfWork unitOfWork)
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
        WktGfaExamples wktGfaExamples = new WktGfaExamples() { Id = 0 };
        bool result = unitOfWork.WktGfaExamplesRepository.Truncate(wktGfaExamples);
        return result;
    }
    #endregion
}
