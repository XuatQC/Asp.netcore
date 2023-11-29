using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.ServicesContracts;
using System.Linq.Expressions;

namespace FNDSystem.Core.Services;
public class ObsFactPastService : GeneticService, IObsFactPastService
{
    private readonly IUnitOfWork unitOfWork;

    public ObsFactPastService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    #region 番号によりOBS観察事実（過去）を取得する
    /// <summary>
    /// 番号によりOBS観察事実（過去）一覧を取得する
    /// </summary>
    /// <param name="num">番号</param>
    /// <returns>OBS観察事実（過去）一覧</returns>
    public IEnumerable<ObsFactPast> GetObsFactPastListByNum(string num)
    {
        Expression<Func<ObsFactPast, bool>> whereExpression = obsFactPast => obsFactPast.Num == num && obsFactPast.PartTrans.ToString().Contains("-1");
        var itemList = unitOfWork.ObsFactPastRepository.GetAll(whereExpression, 0, null).AsEnumerable().OrderBy(x=>x.Num);
        return itemList;
    }
    #endregion
}