using AutoMapper;
using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.ServicesContracts;
using System.Linq.Expressions;

namespace FNDSystem.Core.Services;
public class ObsConclusionService : GeneticService, IObsConclusionService
{
    private readonly IUnitOfWork unitOfWork;

    public ObsConclusionService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
    }

    #region 番号によりOBS添付ファイル（写真）一覧を取得する
    /// <summary>
    /// 番号によりOBS添付ファイル（写真）一覧を取得する
    /// </summary>
    /// <param name="num">番号</param>
    /// <returns>OBS添付ファイル（写真）一覧</returns>
    public IEnumerable<ObsConclusion> GetObsConclusionListByNum(string num)
    {
        Expression<Func<ObsConclusion, bool>> whereExpression = item => item.Num == num;
        var obsConclusionResponseDtoList = unitOfWork.ObsConclusionRepository.GetAll(whereExpression).OrderBy(x => x.DisplayOrder);

        return obsConclusionResponseDtoList;
    }
    #endregion

    #region 番号によりOBS結論を取得する
    /// <summary>
    /// 番号によりOBS結論を取得する
    /// </summary>
    /// <param name="num">番号</param>
    /// <returns>OBS結論</returns>
    public ObsConclusion? GetObsConclusionByNum(string num)
    {
        Expression<Func<ObsConclusion, bool>> whereExp = item => item.Num == num;
        ObsConclusion? obsCon = unitOfWork.ObsConclusionRepository.Get(whereExp);

        return obsCon;
    }

    #endregion
}