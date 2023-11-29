using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.Dto;
using FNDSystem.Core.ServicesContracts;

namespace FNDSystem.Core.Services;
public class WkWordListService : GeneticService, IWkWordListService
{
    private readonly IUnitOfWork unitOfWork;

    public WkWordListService(IUnitOfWork unitOfWork)
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
        WkWordList wkWordList = new WkWordList() { Id = 0 };
        bool result = unitOfWork.WkWordListRepository.Truncate(wkWordList);
        return result;
    }
    #endregion

    #region 印刷データの準備
    /// <summary>
    /// 印刷データの準備
    /// </summary>
    /// <param name="printDto">印刷データ</param>
    /// <returns>印刷データ一覧</returns>
    public IEnumerable<PrintDataResponeseDto>? SetDataListToPrint(FindPrintRequestDto printDto)
    {
        var result = unitOfWork.PrintRepository.SetDataListToPrint(printDto);
        return result;
    }
    #endregion

    #region 一覧並び替え
    /// <summary>
    /// 一覧並び替え
    /// </summary>
    /// <returns>wkword一覧</returns>
    public IEnumerable<WkWordList>? SortDataList()
    {
        var result = unitOfWork.WkWordListRepository.SortDataList();
        return result;
    }
    #endregion
}
