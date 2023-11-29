using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.ServicesContracts;

namespace FNDSystem.Core.Services;
public class WktStrExampleService : GeneticService, IWktStrExampleService
{
    private readonly IUnitOfWork unitOfWork;

    public WktStrExampleService(IUnitOfWork unitOfWork)
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
        WktStrExamples wktStrExamples = new WktStrExamples() { Id = 0 };
        bool result = unitOfWork.WktStrExamplesRepository.Truncate(wktStrExamples);
        return result;
    }
    #endregion
}
