using AutoMapper;
using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.Dto;
using FNDSystem.Core.ServicesContracts;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace FNDSystem.Core.Services;
public class WktObsFactService : GeneticService, IWktObsFactService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public WktObsFactService(IUnitOfWork unitOfWork, IMapper mapper)
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
        Expression<Func<WktObsFact, bool>> whereExp = obsAtt => obsAtt.Id == id;
        WktObsFact? wktObsFact = unitOfWork.WktObsFactRepository.Get(whereExp);
        if (wktObsFact == null) return false;

        unitOfWork.WktObsFactRepository.Remove(wktObsFact);
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
        WktObsFact wktObsFact = new WktObsFact() { Id = 0 };
        bool result = unitOfWork.WktObsFactRepository.Truncate(wktObsFact);
        return result;
    }
    #endregion

    #region 新規作成
    /// <summary>
    /// 新規作成
    /// </summary>
    /// <param name="wktObsFactDto">更新するデータ</param>
    /// <returns>true: 成功します。 false: 失敗します</returns>
    public WktObsFact Create(WktObsFactDto wktObsFactDto)
    {
        var wktObsFact = mapper.Map<WktObsFact>(wktObsFactDto);

        unitOfWork.WktObsFactRepository.Add(wktObsFact);
        unitOfWork.SaveChanges();

        return wktObsFact;
    }
    #endregion
}