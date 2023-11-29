using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.ServicesContracts;
using System.Linq.Expressions;

namespace FNDSystem.Core.Services;
public class SharedFolderService : GeneticService, ISharedFolderService
{
    private readonly IUnitOfWork unitOfWork;
    public SharedFolderService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    /// <summary>
    /// フォルダ情報取得
    /// </summary>
    /// <param name="plantName">検索条件</param>
    /// <returns>共有フォルダ／保存先フォルダマスタ</returns>
    public IEnumerable<SharedFolder>? GetSharedFolderList(string plantName)
    {
        Expression<Func<SharedFolder, bool>> whereExpression = item => item.PlantName == plantName;
        var result = unitOfWork.SharedFolderRepository.GetAll(whereExpression);
        return result;
    }

    /// <summary>
    /// Pposによりフォルダ情報を取得する
    /// </summary>
    /// <param name="plantName">検索条件</param>
    /// <returns>共有フォルダ／保存先フォルダマスタ一覧</returns>
    public IEnumerable<SharedFolder>? GetSharedFolderMenu(string plantName)
    {
        Expression<Func<SharedFolder, bool>> whereExpression = item => item.PlantName == plantName && item.MenuPposi != null;
        var result = unitOfWork.SharedFolderRepository.GetAll(whereExpression).Take(5);
        return result;
    }
}