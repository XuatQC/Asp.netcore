using FNDSystem.Core.Domain;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.ServicesContracts;
using System.Linq.Expressions;

namespace FNDSystem.Core.Services;
public class ControlsService : GeneticService, IControlsService
{
    private readonly IUnitOfWork unitOfWork;

    public ControlsService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    #region 最終同期日の取得

    /// <summary>
    /// 最終同期日の取得
    /// </summary>
    /// <returns>最終同期日</returns>
    public DateTime? GetLastSyncDate()
    {
        Expression<Func<Controls, bool>>? whereExpression = null;

        var lastSyncDate = unitOfWork.ControlsRepository.GetAll(whereExpression, 0, null).FirstOrDefault()?.LastSyncDate;
        DateTime? result = new DateTime(2029, 12, 31, 00, 00, 00, DateTimeKind.Utc);
        if (lastSyncDate != null) result = lastSyncDate;
        return result;
    }

    #endregion
}