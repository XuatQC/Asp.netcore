using FNDSystem.Core.Domain;
using FNDSystem.Core.Domain.RepositoryContracts;
using System.Linq.Expressions;

namespace FNDSystem.Core.Services;
public class ActivityService : GeneticService, IActivityService
{
    private readonly IUnitOfWork unitOfWork;

    public ActivityService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    #region アクティビティ一覧の取得
    /// <summary>
    /// アクティビティ一覧の取得
    /// </summary>
    /// <param name="plantName">プラント名</param>
    /// <returns>「観察対象マスタ」リスト</returns>
    public IEnumerable<Activity>? GetActivityList(string plantName)
    {
        List<Activity> result = new();
        Expression<Func<Activity, bool>>? whereExpression = item => item.PlantName == plantName;
        var list = unitOfWork.ActivityRepository.GetAll(whereExpression, 0, null);
        if (list != null && list?.Count() > 0) result = list.OrderBy(x => x.DisplayOrder).ToList();

        return result;
    }
    #endregion
}