using AutoMapper;
using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.ServicesContracts;
using Microsoft.AspNetCore.Http;

namespace FNDSystem.Core.Services;
public class WktAnalysisWcService : GeneticService, IWktAnalysisWcService
{
    private readonly IUnitOfWork unitOfWork;

    public WktAnalysisWcService(IUnitOfWork unitOfWork)
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
        WktAnalysisWc wktAnalysisWc = new WktAnalysisWc() { Id = 0 };
        bool result = unitOfWork.WktAnalysisWcRepository.Truncate(wktAnalysisWc);
        return result;
    }
    #endregion
}
