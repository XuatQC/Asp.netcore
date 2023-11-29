using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.Dto;
using Microsoft.EntityFrameworkCore;

namespace FNDSystem.Infrastructure.DataAccess.Repositories;
public class WktTransHistoryRepository : GenericRepository<WktTransHistory>, IWktTransHistoryRepository
{
    public WktTransHistoryRepository(DBMasterContext masterDBContext, DBSlaveContext slaveDBContext) : base(masterDBContext, slaveDBContext)
    {

    }

    #region 子テーブル1（SQL生成_子テーブル1）により履歴一時表のデータを挿入する
    /// <summary>
    /// 子テーブル1（SQL生成_子テーブル1）により履歴一時表のデータを挿入する
    /// </summary>
    /// <param name="pDisplayOrder">表示順</param>
    /// <param name="pItemName">項目名</param>
    /// <param name="pItemNameJp">項目名英文</param>
    /// <param name="pItemNameEn">項目名翻訳</param>
    /// <param name="pPlantName">プラント名</param>
    /// <param name="pKinds">種類</param>
    /// <param name="pField">分野</param>
    /// <param name="pPartId">個別ID</param>
    /// <param name="pSerialNum">通番</param>
    /// <param name="pRevisions">リビジョン</param>
    /// <param name="pEnglishEdition">英語版</param>
    public void InsertByScope(  int pDisplayOrder
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
        this.masterDBContext?.Database.ExecuteSql($@"CALL Sp_InsertWktTransHistoryByObsExtRefAndScope(  {pDisplayOrder}
                                                                                                        ,{pItemName}
                                                                                                        ,{pItemNameJp}
                                                                                                        ,{pItemNameEn}
                                                                                                        ,{pPlantName}
                                                                                                        ,{pKinds}
                                                                                                        ,{pField}
                                                                                                        ,{pPartId}
                                                                                                        ,{pSerialNum}
                                                                                                        ,{pRevisions}
                                                                                                        ,{pEnglishEdition});");
    }
    #endregion

    #region 拡張履歴テーブルのデータを取得する
    /// <summary>
    /// 拡張履歴テーブルのデータを取得する
    /// </summary>
    /// <param name="pParentTableName"></param>
    /// <param name="pChildTableName"></param>
    /// <param name="pItemName"></param>
    /// <param name="pItemNameJp"></param>
    /// <param name="pItemNameEn"></param>
    /// <param name="pPlantName"></param>
    /// <param name="pKinds"></param>
    /// <param name="pField"></param>
    /// <param name="pPartId"></param>
    /// <param name="pSerialNum"></param>
    /// <param name="pRevisions"></param>
    /// <param name="pEnglishEdition"></param>
    /// <returns></returns>
    public List<WktTransHistoryExtendDto>? GetWktTransHistoryByParentAndChildTableName(string pParentTableName
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

        var result = this.slaveDBContext?.WktTransHistoryExtendDtos?
                         .FromSql($@"Call Sp_Get_WktTransHistoryByParentAndChildTableName({pParentTableName}
                                                                                        ,{pChildTableName}
                                                                                        ,{pItemName}
                                                                                        ,{pItemNameJp}
                                                                                        ,{pItemNameEn}
                                                                                        ,{pPlantName}
                                                                                        ,{pKinds}
                                                                                        ,{pField}
                                                                                        ,{pPartId}
                                                                                        ,{pSerialNum}
                                                                                        ,{pRevisions}
                                                                                        ,{pEnglishEdition});")
                        .ToList();

        return result;
    }
    #endregion
}