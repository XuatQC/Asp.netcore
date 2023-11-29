using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.Dto;
using FNDSystem.Core.ServicesContracts;

namespace FNDSystem.Core.Services;
public class PrintService : GeneticService, IPrintService
{
    private readonly IUnitOfWork unitOfWork;

    public PrintService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }


    #region Word出力処理の準備
    /// <summary>
    /// Word出力処理の準備
    /// </summary>
    /// <param name="findTranslateRequestDto">検索条件</param>
    /// <returns>true: 成功します。 false: 失敗します</returns>
    public List<VwTranslateControl>? PreOutputWord(FindTranslateRequestDto findTranslateRequestDto)
    {

        var translateList = unitOfWork.PrintRepository.PreOutputWord(findTranslateRequestDto);

        return translateList;
    }
    #endregion

    #region Word出力処理
    /// <summary>
    /// Word出力処理
    /// </summary>
    /// <param name="inLang">ラング</param>
    /// <param name="preTranslateList">事前翻訳リスト</param>
    /// <param name="obsPathsDto">検索条件</param>
    /// <returns>true: 成功します。 false: 失敗します</returns>
    public bool ProcessingOutputWord(string inLang, List<VwTranslateControl> preTranslateList, ObsPathsDto obsPathsDto)
    {
        var result = unitOfWork.PrintRepository.ProcessingOutputWord(inLang, preTranslateList, obsPathsDto);

        return result;
    }
    #endregion

}