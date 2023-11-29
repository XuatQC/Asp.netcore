using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.ServicesContracts;
using System.Linq.Expressions;

namespace FNDSystem.Core.Services;
public class ObsConclusionPastService : GeneticService, IObsConclusionPastService
{
    private readonly IUnitOfWork unitOfWork;

    public ObsConclusionPastService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    #region 番号によりOBS結論（過去）一覧を取得する
    /// <summary>
    /// 番号によりOBS結論（過去）一覧を取得する
    /// </summary>
    /// <param name="num">番号</param>
    /// <returns>OBS結論（過去）一覧</returns>
    public IEnumerable<ObsConclusionPast> GetObsConclusionPastListByNum(string num)
    {
        Expression<Func<ObsConclusionPast, bool>> whereExpression = item => item.Num == num;
        var itemList = unitOfWork.ObsConclusionPastRepository.GetAll(whereExpression, 0, null);

        return itemList;
    }
    #endregion
}