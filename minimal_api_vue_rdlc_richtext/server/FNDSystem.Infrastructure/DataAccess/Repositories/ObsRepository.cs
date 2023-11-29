using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.Dto;
using FNDSystem.Core.Dto.OBS;
using FNDSystem.Core.Helpers;
using Microsoft.EntityFrameworkCore;

namespace FNDSystem.Infrastructure.DataAccess.Repositories;
public class ObsRepository : GenericRepository<Obs>, IObsRepository
{
    public ObsRepository(DBMasterContext masterDBContext, DBSlaveContext slaveDBContext) : base(masterDBContext, slaveDBContext)
    {
    }

    #region obs

    #region GetObsRevisionData

    public IEnumerable<ObsExtend>? GetObsExtends(string num)
    {
        return slaveDBContext.ObsExtends?.Where(x => x.Num == num && !x.DeleteFlag);
    }

    /// <summary>
    /// OBS一覧取得
    /// </summary>
    /// <param name="findObsDto">検索条件</param>
    /// <returns>OBS一覧</returns>
    public IEnumerable<NumberControl>? GetObsRevisionList(FindObsRequestDto findObsDto)
    {
        var numberControlList = this.slaveDBContext?.NumberControls?
                          .Where(x => x.PlantName == findObsDto.PlantName &&
                                      x.Kinds == findObsDto.Kinds &&
                                      x.Fields == findObsDto.Fields &&
                                      x.PartId == findObsDto.PartId &&
                                      x.SerialNum == findObsDto.SerialNum &&
                                      x.Revisions < findObsDto.Revisions &&
                                      x.Language == findObsDto.Language &&
                                      !x.DeleteFlag)
                           .OrderByDescending(x => x.Revisions)
                           .ToList();

        return numberControlList;
    }
    #endregion

    #region 番号によりOBS一覧を取得する

    /// <summary>
    /// 番号によりOBS一覧を取得する
    /// </summary>
    /// <param name="findObsDto">検索条件</param>
    /// <returns>OBS</returns>
    public IEnumerable<Obs>? GetObsListByMultiCodition(FindObsRequestDto findObsDto)
    {
        var obsList = this.slaveDBContext?.Obss?.Where(x => x.Num == findObsDto.Num).ToList();
        return obsList;
    }
    #endregion

    #region ObsExtend一覧取得
    /// <summary>
    /// ObsExtend一覧取得
    /// </summary>
    /// <param name="obsExtendDto">検索条件</param>
    /// <returns>OBS一覧</returns>
    public IEnumerable<DisObsReponseDto>? GetObsExtendList(FindObsRequestDto obsExtendDto)
    {
        ObjectUtils.NullToDefault<FindObsRequestDto>(obsExtendDto);
        var obsList = this.slaveDBContext?.DisObs?
                          .FromSql($@"Call Sp_Get_Obs_Extend_List_Search ({obsExtendDto.PlantName},
                                                                      {obsExtendDto.Fields},
                                                                      {obsExtendDto.PartId},
                                                                      {obsExtendDto.Language},
                                                                      {obsExtendDto.ChkTl},
                                                                      {obsExtendDto.FreeWord});")
                          .ToList();

        return obsList;
    }
    #endregion

    #region OBS新規作成
    /// <summary>
    /// OBS新規作成
    /// </summary>
    /// <param name="obsExtendDto">obs</param>
    /// <returns>obs</returns>
    public Obs? AddNewObsAtScreenList(FindObsRequestDto obsExtendDto)
    {
        ObjectUtils.NullToDefault<FindObsRequestDto>(obsExtendDto);
        var obs = this.slaveDBContext?.Obss?
                                      .FromSql($@"Call Sp_Creat_New_Obs ({obsExtendDto.PlantName},
                                                                         {obsExtendDto.DmDivision},
                                                                         {obsExtendDto.PartId},
                                                                         {obsExtendDto.Fields},
                                                                         {obsExtendDto.Language});")
                                      .AsEnumerable()
                                      .FirstOrDefault();
        return obs;
    }
    #endregion

    #region クリアボタン押下時にObsExtend一覧を取得する
    /// <summary>
    /// クリアボタン押下時にObsExtend一覧を取得する
    /// </summary>
    /// <param name="obsExtendDto">検索条件</param>
    /// <returns>OBS一覧</returns>
    public IEnumerable<DisObsReponseDto>? GetObsExtendListWhenClearClick(FindObsRequestDto obsExtendDto)
    {
        var obsList = this.slaveDBContext?.DisObs?
                          .FromSql($@"Call Sp_Get_Obs_Extend_List_Clear ({obsExtendDto.PlantName},{obsExtendDto.Language});")
                          .ToList();

        return obsList;
    }
    #endregion

    #region GetNum
    /// <summary>
    /// OBSの番号とタイトルを取得する
    /// </summary>
    /// <param name="plantName">検索条件</param>
    /// <param name="fields">検索条件</param>
    /// <returns>List of NumObsDto</returns>
    public IEnumerable<NumObsDto>? GetNum(string plantName, string fields)
    {
        var resultList =
            slaveDBContext?.VwObsControls?
            .Where(
                x => x.Num == plantName + fields &&
                x.NewestFlag == true && !x.DeleteFlag && x.Hold == true
            ).AsEnumerable()
            .Select((x) => new NumObsDto()
            {
                Num = x.Num,
                Title = x.Title
            }).OrderBy(x => x.Num);

        return resultList;
    }
    #endregion

    #region 論理削除

    /// <summary>
    /// 論理削除
    /// </summary>
    /// <param name="deleteObsDto"></param>
    /// <returns></returns>
    public bool Delete(DeleteObsDto deleteObsDto)
    {
        var result = this.masterDBContext?.Database.ExecuteSql($@"UPDATE tbl_obs 
                                                                  SET delete_flag = true,
                                                                      last_update = {DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")},
                                                                      last_user   = {deleteObsDto.LastUser}
                                                                  WHERE num = {deleteObsDto.Num} AND delete_flag = false;");
        return (result > 0);
    }
    #endregion

    #region wktテーブルのデータを削除する

    /// <summary>
    /// wktテーブルのデータを削除する
    /// </summary>
    /// <returns></returns>
    public bool DeleteDataOfWktTables()
    {
        var result = this.masterDBContext?.Database.ExecuteSql($@"CALL Sp_Delete_Data_Of_WkTables();");
        return result > 0;
    }
    #endregion

    #region MAX数値を取得する
    /// <summary>
    /// MAX数値を取得する
    /// </summary>
    /// <param name="obsDto">検索条件</param>
    /// <returns>MAX数値</returns>
    public int GetMaxNo(FindObsRequestDto obsDto)
    {
        int maxId = 1;
        var item = this.slaveDBContext?.Database.SqlQuery<int>(@$"Select Fn_Get_Max_No({obsDto.PlantName},{obsDto.Fields},{obsDto.DmDivision},{obsDto.PartId});").AsEnumerable();
        if (item != null) maxId = item.ElementAt(0);
        return maxId;
    }
    #endregion

    #region 翻訳依頼２へのOBS一覧を取得する
    /// <summary>
    /// 翻訳依頼２へのOBS一覧を取得する
    /// </summary>
    /// <param name="obsDto">検索条件</param>
    /// <returns>OBS一覧</returns>
    public IEnumerable<Obs>? GetObsListForReqTran2(FindObsRequestDto obsDto)
    {
        var obsList = this.slaveDBContext?.Obss?
                          .FromSql($@"Call Sp_Get_Obs_List_For_Req_Tran2 ({obsDto.PlantName},{obsDto.Kinds},
                                                                          {obsDto.Fields},{obsDto.PartId},
                                                                          {obsDto.SerialNum},{obsDto.Revisions},
                                                                          {obsDto.Language});")
                        .ToList();
        return obsList;
    }

    #endregion

    #endregion

    #region リクエスト番号取得
    /// <summary>
    /// ObsExtendReferの番号を取得する
    /// </summary>
    /// <param name="findObsDto">検索条件</param>
    /// <returns>番号</returns>
    public string GetReqNumOfObsExtendRefer(FindObsRequestDto findObsDto)
    {
        string result = string.Empty;
        List<ObsExtendReferDto>? ObsExtendReferList = GetTblObsExtendRefer();

        ObsExtendReferDto? obsExtendReferDto = ObsExtendReferList?.Where(x => x.PlantName == findObsDto.PlantName &&
                                                                              x.Kinds == findObsDto.Kinds &&
                                                                              x.Fields == findObsDto.Fields &&
                                                                              x.PartId == findObsDto.PartId &&
                                                                              x.SerialNum == findObsDto.SerialNum &&
                                                                              x.Revisions == findObsDto.Revisions &&
                                                                              x.Language == findObsDto.Language).AsEnumerable().FirstOrDefault();
        if (obsExtendReferDto != null && obsExtendReferDto.Num != null) result = obsExtendReferDto.Num;

        return result;
    }

    /// <summary>
    /// 番号によりリクエスト番号を取得する
    /// </summary>
    /// <param name="findObsExtendReferDto"></param>
    /// <returns></returns>
    public string GetReqNumOfObsExtendReferByNum(FindObsExtendReferRequestDto findObsExtendReferDto)
    {
        string result = string.Empty;
        List<ObsExtendReferDto>? obsExtendReferList = GetTblObsExtendRefer();

        ObsExtendReferDto? obsExtendReferDto = obsExtendReferList?.Find(x => x.Num == findObsExtendReferDto.Num);
        if (obsExtendReferDto != null && obsExtendReferDto.Num != null) result = obsExtendReferDto.Num;

        return result;
    }

    /// <summary>
    /// 番号によりObsExtendRefer一覧を取得する
    /// </summary>
    /// <param name="findObsExtendReferDto">検索条件</param>
    /// <returns>OBSExtend一覧</returns>
    public IEnumerable<ObsExtendReferDto>? GetObsExtendReferListByNum(FindObsExtendReferRequestDto findObsExtendReferDto)
    {
        var result = GetTblObsExtendRefer();
        List<ObsExtendReferDto>? obsExtendReferList = result?.Where(x => x.Num == findObsExtendReferDto.Num).ToList();
        return obsExtendReferList;
    }

    /// <summary>
    /// 言語によりobsextendrefer一覧を取得する
    /// </summary>
    /// <returns>OBSExtend一覧</returns>
    public List<ObsExtendReferDto>? GetTblObsExtendRefer()
    {
        List<ObsExtendReferDto>? obsExtendReferList = this.slaveDBContext?.ObsExtendRefers?
                                                          .FromSql($@"Select * from vw_obs_extend_refer;")
                                                          .ToList();
        return obsExtendReferList;

    }
    #endregion
}