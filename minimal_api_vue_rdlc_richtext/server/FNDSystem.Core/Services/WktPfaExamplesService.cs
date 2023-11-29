using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.ServicesContracts;

namespace FNDSystem.Core.Services;
public class WktPfaExamplesService : GeneticService, IWktPfaExamplesService
{
    private readonly IUnitOfWork unitOfWork;

    public WktPfaExamplesService(IUnitOfWork unitOfWork)
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
        WktPfaExamples wktPfaExamples = new WktPfaExamples() { Id = 0};
        bool result = unitOfWork.WktPfaExamplesRepository.Truncate(wktPfaExamples);
        return result;
    }
    #endregion
}
