using AutoMapper;
using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.ServicesContracts;
using Microsoft.AspNetCore.Http;

namespace FNDSystem.Core.Services;
public class WktAnalysisObsService : GeneticService, IWktAnalysisObsService
{
    private readonly IUnitOfWork unitOfWork;

    public WktAnalysisObsService(IUnitOfWork unitOfWork)
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
        WktAnalysisObs wktAnalysisObs = new WktAnalysisObs() { Id = 0 };
        bool result = unitOfWork.WktAnalysisObsRepository.Truncate(wktAnalysisObs);
        return result;
    }
    #endregion
}
