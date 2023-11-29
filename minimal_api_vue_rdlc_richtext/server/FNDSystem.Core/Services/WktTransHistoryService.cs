using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.Dto;
using FNDSystem.Core.Helpers;
using FNDSystem.Core.ServicesContracts;
using System.Linq.Expressions;

namespace FNDSystem.Core.Services;
public class WktTransHistoryService : GeneticService, IWktTransHistoryService
{
    private readonly IUnitOfWork unitOfWork;

    public WktTransHistoryService(IUnitOfWork unitOfWork)
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
        WktTransHistory wktTransHistory = new WktTransHistory() { Id = 0 };
        bool result = unitOfWork.WktTransHistoryRepository.Truncate(wktTransHistory);
        return result;
    }
    #endregion

    #region 翻訳履歴一覧を取得する
    /// <summary>
    /// 翻訳履歴一覧を取得する
    /// </summary>
    /// <param name="findWktTranslateDto">条件検索</param>
    /// <returns>翻訳履歴一覧</returns>
    public IEnumerable<WktTransHistory>? GetMany(FindWktTransHistoryRequest findWktTranslateDto)
    {
        Expression<Func<WktTransHistory, bool>>? whereExpression = null;
        if (!string.IsNullOrEmpty(findWktTranslateDto.Translated))
        {
            whereExpression = item => item.Translated == findWktTranslateDto.Translated;
        }

        var result = unitOfWork.WktTransHistoryRepository.GetAll(whereExpression).OrderBy(x => x.DisplayOrder).ThenBy(x => x.Revisions);

        return result;
    }
    #endregion

    #region 履歴一時表のデータを処理する
    /// <summary>
    /// 履歴一時表のデータを処理する
    /// </summary>
    /// <param name="wktTransHistoryRequest">翻訳履歴データのリクエスト</param>
    /// <returns>true: 成功します。 false: 失敗します</returns>
    public bool ProcessDataHandler(WktTransHistoryRequest wktTransHistoryRequest)
    {
        string parentTableName = string.Empty;
        switch (wktTransHistoryRequest.Kinds)
        {
            case Constants.KIND_WC:
                break;
            case Constants.KIND_OBS:
                parentTableName = "vw_obs_extend_refer";
                InsertByParentTable(parentTableName, 100, Constants.OBS_INPUT_LABEL_TITLE, "title", "title_trans", "part_trans", wktTransHistoryRequest);
                InsertByScopeTran(200, Constants.OBS_INPUT_LABEL_SCOPE, "scope", "scope_trans", wktTransHistoryRequest);
                InsertByChildTable2(parentTableName, 300, "tbl_obs_fact", Constants.OBS_INPUT_LABEL_FACT, "fact", "fact_trans", wktTransHistoryRequest);
                InsertByChildTable2(parentTableName, 400, "tbl_obs_conclusion", Constants.OBS_INPUT_LABEL_CONCLUSION, "conclusion", "conclusion_trans", wktTransHistoryRequest);
                break;
            case Constants.KIND_GFA:
                break;
            case Constants.KIND_PFA:
                break;
            case Constants.KIND_AFI:
                break;
            case Constants.KIND_PD:
                break;
            case Constants.KIND_STR:
                break;
            case Constants.KIND_BP:
                break;
            case Constants.KIND_SOI:
                break;
        }
        return true;
    }
    #endregion

    #region 親テーブルにより履歴一時表のデータを挿入する
    /// <summary>
    /// 親テーブルにより履歴一時表のデータを挿入する
    /// </summary>
    /// <param name="pDisplayOrder">表示順</param>
    /// <param name="pItemName">項目名</param>
    /// <param name="pItemNameJap">項目名和文</param>
    /// <param name="pItemNameEng">項目名英文</param>
    /// <param name="pItemNameTran">項目名翻訳</param>
    private void InsertByParentTable(string pParentTableName,
                                     int pDisplayOrder, string pItemName,
                                     string pItemNameJap, string pItemNameEng,
                                     string pItemNameTran,
                                     WktTransHistoryRequest wktTransHistoryRequest)
    {
        // insert WktTransHistory
        WktTransHistory inWktTransHistory;
        List<WktTransHistory> wktTransHistoryList = new List<WktTransHistory>();
        switch (pParentTableName)
        {
            case "":
                break;
            case "vw_obs_extend_refer":
                var obsExtenReferList = unitOfWork.ObsRepository.GetTblObsExtendRefer();
                var obsExtendReferDtoInsList = obsExtenReferList?.Where(x => x.PlantName == wktTransHistoryRequest.PlantName &&
                                                                             x.Kinds == wktTransHistoryRequest.Kinds &&
                                                                             x.Fields == wktTransHistoryRequest.Fields &&
                                                                             x.PartId == wktTransHistoryRequest.PartId &&
                                                                             x.SerialNum == wktTransHistoryRequest.SerialNum &&
                                                                             x.Language == wktTransHistoryRequest.EnglishEdition).AsEnumerable().OrderBy(x => x.Revisions);
                if (obsExtendReferDtoInsList != null)
                {
                    foreach (var item in obsExtendReferDtoInsList)
                    {
                        inWktTransHistory = new();
                        inWktTransHistory.Id = 0;
                        inWktTransHistory.Revisions = Convert.ToInt32(item.Revisions);
                        inWktTransHistory.Item = pItemName;
                        if (item.TranslatedState && (item.TransRange == Convert.ToString((int)Enums.TransRange.FULL_TEXT) || (item.TransRange == Convert.ToString((int)Enums.TransRange.PART) && Convert.ToBoolean(pItemNameTran))))
                        {
                            inWktTransHistory.Japanese = pItemNameJap == "title" ? item.Title : string.Empty;
                        }
                        else
                        {
                            inWktTransHistory.Japanese = string.Empty;
                        }

                        if (item.TranslatedState && (item.TransRange == Convert.ToString((int)Enums.TransRange.FULL_TEXT) || (item.TransRange == Convert.ToString((int)Enums.TransRange.PART) && Convert.ToBoolean(pItemNameTran))))
                        {
                            inWktTransHistory.English = pItemNameEng == "title_trans" ? item.TitleTrans : string.Empty;
                        }
                        else
                        {
                            inWktTransHistory.English = string.Empty;
                        }
                        if (item.TranslatedState && (item.TransRange == Convert.ToString((int)Enums.TransRange.FULL_TEXT)))
                        {
                            inWktTransHistory.Translated = Constants.TRANS_FULL_TEXT;
                        }
                        else
                        {
                            if (item.TranslatedState && (item.TransRange == Convert.ToString((int)Enums.TransRange.PART)) && Convert.ToBoolean(pItemNameTran))
                            {
                                inWktTransHistory.Translated = Constants.TRANS_PARTIAL_TEXT;
                            }
                            else
                            {
                                inWktTransHistory.Translated = string.Empty;
                            }
                        }

                        inWktTransHistory.DisplayOrder = pDisplayOrder;
                        wktTransHistoryList.Add(inWktTransHistory);
                    }
                }
                break;
            case Constants.KIND_GFA:
                break;
            case Constants.KIND_PFA:
                break;
            case Constants.KIND_AFI:
                break;
            case Constants.KIND_PD:
                break;
            case Constants.KIND_STR:
                break;
            case Constants.KIND_BP:
                break;
            case Constants.KIND_SOI:
                break;
        }
        if (wktTransHistoryList.Count > 0)
        {
            unitOfWork.WktTransHistoryRepository.AddRange(wktTransHistoryList);
            unitOfWork.SaveChanges();
        }
    }
    #endregion

    #region 子テーブル1（SQL生成_子テーブル1）により履歴一時表のデータを挿入する
    /// <summary>
    /// 子テーブル1（SQL生成_子テーブル1）により履歴一時表のデータを挿入する
    /// </summary>
    /// <param name="pDisplayOrder">表示順</param>
    /// <param name="pItemName">項目名</param>
    /// <param name="pItemNameJp">項目名英文</param>
    /// <param name="pItemNameEn">項目名翻訳</param>
    /// <param name="wktTransHistoryRequest">取引履歴テンプレートのリクエスト</param>
    private void InsertByScopeTran( int pDisplayOrder
                                    , string pItemName
                                    , string pItemNameJp
                                    , string pItemNameEn
                                    , WktTransHistoryRequest wktTransHistoryRequest)
    {
        unitOfWork.WktTransHistoryRepository.InsertByScope( pDisplayOrder
                                                                , pItemName
                                                                , pItemNameJp
                                                                , pItemNameEn
                                                                , wktTransHistoryRequest.PlantName
                                                                , wktTransHistoryRequest.Kinds
                                                                , wktTransHistoryRequest.Fields
                                                                , wktTransHistoryRequest.PartId
                                                                , wktTransHistoryRequest.SerialNum
                                                                , wktTransHistoryRequest.Revisions
                                                                , wktTransHistoryRequest.EnglishEdition);
        unitOfWork.SaveChanges();
    }
    #endregion

    #region 子テーブル2（SQL生成_子テーブル2）により履歴一時表のデータを挿入する
    /// <summary>
    /// 子テーブル2（SQL生成_子テーブル2）により履歴一時表のデータを挿入する
    /// </summary>
    /// <param name="pParentTableName">親テーブル名</param>
    /// <param name="pDisplayOrder">表示順</param>
    /// <param name="pChildTableName">子テーブル名</param>
    /// <param name="pItemName">項目名</param>
    /// <param name="pItemNameJp">項目名英文</param>
    /// <param name="pItemNameEn">項目名翻訳</param>
    /// <param name="wktTransHistoryRequest">取引履歴テンプレートのリクエスト</param>
    private void InsertByChildTable2(string pParentTableName, int pDisplayOrder, string pChildTableName, string pItemName, string pItemNameJp, string pItemNameEn, WktTransHistoryRequest wktTransHistoryRequest)
    {
        // WktTransHistoryを挿入
        List<WktTransHistory> wktTransHistorieList = new();
        WktTransHistory inWktTransHistory;

        List<WktTransHistoryExtendDto>? insertList = GetWktTransHistoryByParentAndChildTableName(pParentTableName
                                                                                               , pChildTableName
                                                                                               , pItemName
                                                                                               , pItemNameJp
                                                                                               , pItemNameEn
                                                                                               , wktTransHistoryRequest.PlantName
                                                                                               , wktTransHistoryRequest.Kinds
                                                                                               , wktTransHistoryRequest.Fields
                                                                                               , wktTransHistoryRequest.PartId
                                                                                               , wktTransHistoryRequest.SerialNum
                                                                                               , wktTransHistoryRequest.Revisions
                                                                                               , wktTransHistoryRequest.EnglishEdition);
        if (insertList != null)
        {
            int i = 1;
            foreach (WktTransHistoryExtendDto wktTransHistoryExtendDto in insertList)
            {
                inWktTransHistory = new();
                inWktTransHistory.Id = 0;
                inWktTransHistory.Item = pItemName + "(" + i.ToString() + ")";
                inWktTransHistory.Revisions = Convert.ToInt32(wktTransHistoryExtendDto.Revisions);

                if (wktTransHistoryExtendDto != null && (wktTransHistoryExtendDto.TranslatedState
                    && wktTransHistoryExtendDto?.TransRange == Convert.ToString((int)Enums.TransRange.FULL_TEXT))
                    || (wktTransHistoryExtendDto?.TransRange == Convert.ToString((int)Enums.TransRange.PART) && !Convert.ToBoolean(wktTransHistoryExtendDto.PartTrans)))
                {
                    inWktTransHistory.English = wktTransHistoryExtendDto.ItemNameEn;
                    inWktTransHistory.Japanese = wktTransHistoryExtendDto.ItemNameJp;
                    if (wktTransHistoryExtendDto.TransRange == Convert.ToString((int)Enums.TransRange.FULL_TEXT))
                    {
                        inWktTransHistory.Translated = Constants.TRANS_FULL_TEXT;
                    }
                    else if (wktTransHistoryExtendDto.TransRange == Convert.ToString((int)Enums.TransRange.FULL_TEXT) && !Convert.ToBoolean(wktTransHistoryExtendDto.PartTrans))
                    {
                        inWktTransHistory.Translated = Constants.TRANS_PARTIAL_TEXT;
                    }
                    else
                    {
                        inWktTransHistory.Translated = string.Empty;
                    }

                    inWktTransHistory.DisplayOrder = pDisplayOrder + i;
                    wktTransHistorieList.Add(inWktTransHistory);
                }
                i++;
            }

            if (wktTransHistorieList.Count > 0)
            {
                unitOfWork.WktTransHistoryRepository.AddRange(wktTransHistorieList);
                unitOfWork.SaveChanges();
            }
        }
    }
    #endregion

    #region 拡張履歴テーブルのデータを取得する
    /// <summary>
    /// 拡張履歴テーブルのデータを取得する
    /// </summary>
    /// <param name="pParentTableName">親テーブル名</param>
    /// <param name="pChildTableName">子テーブル名</param>
    /// <param name="pItemName">項目名</param>
    /// <param name="pItemNameJp">アイテム名日本</param>
    /// <param name="pItemNameEn">項目名 英語</param>
    /// <param name="pPlantName">プラント名</param>
    /// <param name="pKinds">種類</param>
    /// <param name="pField">分野</param>
    /// <param name="pPartId">個別ID</param>
    /// <param name="pSerialNum">通番</param>
    /// <param name="pRevisions">リビジョン</param>
    /// <param name="pEnglishEdition">英語版</param>
    /// <returns> 拡張履歴テーブル</returns>
    private List<WktTransHistoryExtendDto>? GetWktTransHistoryByParentAndChildTableName(string pParentTableName
                                                                                       , string pChildTableName
                                                                                       , string pItemName
                                                                                       , string pItemNameJp
                                                                                       , string pItemNameEn
                                                                                       , string? pPlantName
                                                                                       , string? pKinds
                                                                                       , string? pField
                                                                                       , string? pPartId
                                                                                       , int? pSerialNum
                                                                                       , int? pRevisions
                                                                                       , string? pEnglishEdition)
    {
        var result = unitOfWork.WktTransHistoryRepository.GetWktTransHistoryByParentAndChildTableName(pParentTableName
                                                                                                        , pChildTableName
                                                                                                        , pItemName
                                                                                                        , pItemNameJp
                                                                                                        , pItemNameEn
                                                                                                        , pPlantName
                                                                                                        , pKinds
                                                                                                        , pField
                                                                                                        , pPartId
                                                                                                        , pSerialNum
                                                                                                        , pRevisions
                                                                                                        , pEnglishEdition);
        return result;
    }
    #endregion
}
