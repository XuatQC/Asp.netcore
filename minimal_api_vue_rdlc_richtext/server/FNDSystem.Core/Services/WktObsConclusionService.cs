using AutoMapper;
using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.Dto;
using FNDSystem.Core.ServicesContracts;
using System.Linq.Expressions;

namespace FNDSystem.Core.Services;
public class WktObsConclusionService : GeneticService, IWktObsConclusionService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public WktObsConclusionService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    #region IDで削除する
    /// <summary>
    /// IDで削除する
    /// </summary>
    /// <param name="id">ID</param>
    /// <returns>true: 成功します。 false: 失敗します</returns>
    public bool DeleteById(int id)
    {
        Expression<Func<WktObsConclusion, bool>> whereExp = wktObsCon => wktObsCon.Id == id;
        WktObsConclusion? wktObsCon = unitOfWork.WktObsConclusionRepository.Get(whereExp);
        if (wktObsCon == null) return false;

        unitOfWork.WktObsConclusionRepository.Remove(wktObsCon);
        unitOfWork.SaveChanges();
        return true;
    }
    #endregion

    #region データ削除
    /// <summary>
    /// データ削除
    /// </summary>
    /// <returns>true: 成功します。 false: 失敗します</returns>
    public bool RemoveAll()
    {
        WktObsConclusion wktObsConclusion = new WktObsConclusion() { Id = 0 };
        bool result = unitOfWork.WktObsConclusionRepository.Truncate(wktObsConclusion);
        return result;
    }
    #endregion

    #region データの作成
    /// <summary>
    /// データの作成
    /// </summary>
    /// <param name="wktObsConclusionDto">更新するデータ</param>
    /// <returns>新しいOBS結論テーブル/returns>
    public WktObsConclusion Create(WktObsConclusionDto wktObsConclusionDto)
    {
        var wktObsCon = mapper.Map<WktObsConclusion>(wktObsConclusionDto);

        unitOfWork.WktObsConclusionRepository.Add(wktObsCon);
        unitOfWork.SaveChanges();

        return wktObsCon;
    }
    #endregion
}