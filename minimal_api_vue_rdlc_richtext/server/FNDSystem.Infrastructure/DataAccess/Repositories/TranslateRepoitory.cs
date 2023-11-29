using FNDSystem.Core;
using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.Dto;
using FNDSystem.Core.Helpers;
using Microsoft.EntityFrameworkCore;

namespace FNDSystem.Infrastructure.DataAccess.Repositories;
public class TranslateRepoitory : GenericRepository<Translate>, ITranslateRepository
{
    public TranslateRepoitory(DBMasterContext masterDBContext, DBSlaveContext slaveDBContext) : base(masterDBContext, slaveDBContext)
    {

    }

    #region OfferNum一覧を取得する
    /// <summary>
    /// OfferNum一覧を取得する
    /// </summary>
    /// <param name="findTranslateDto">検索条件</param>
    /// <returns>OfferNum一覧</returns>
    public List<OfferNumResponseDto>? GetOfferNumList(FindTranslateRequestDto findTranslateDto)
    {
        var result = this.slaveDBContext?.OfferNumResponseDtos?
                         .FromSql($@"Call Sp_Get_Num_List({findTranslateDto.PlantName},{findTranslateDto.LNum});")
                         .ToList();

        return result;
    }
    #endregion

    #region 翻訳データを作成する
    /// <summary>
    /// 翻訳データ作成
    /// </summary>
    /// <param name="findTranslateDto">WORDファイル格納情報ファイル</param>
    /// <param name="pTableName">テーブル名</param>
    /// <param name="gPlantName">プラント名</param>
    /// <returns>true: 成功します。 false: 失敗します</returns>
    public bool CreateTranslationData(FindTranslateRequestDto findTranslateDto, string pTableName, string gPlantName)
    {
        bool result = false;
        string strSQL = string.Empty;
        string strTableName = string.Empty;
        string serial_branch_num = string.Empty;
        switch (pTableName.ToUpper())
        {
            case "WC":
                strTableName = "tbl_wc";
                serial_branch_num = "serial_num as serial_branch_num";
                break;
            case "OBS":
                strTableName = "vw_obs_control";
                serial_branch_num = "serial_num as serial_branch_num";
                break;
            case "GFA":
                strTableName = "tbl_gfa";
                serial_branch_num = "branch_num as serial_branch_num";
                break;
            case "PFA":
                strTableName = "tbl_pfa";
                serial_branch_num = "branch_num as serial_branch_num";
                break;
            case "AFI":
                strTableName = "tbl_afi";
                serial_branch_num = "serial_num as serial_branch_num";
                break;
            case "PD":
                strTableName = "tbl_pd";
                serial_branch_num = "serial_num as serial_branch_num";
                break;
            case "STR":
                strTableName = "tbl_str";
                serial_branch_num = "branch_num as serial_branch_num";
                break;
            case "BP":
                strTableName = "tbl_bp";
                serial_branch_num = "branch_num as serial_branch_num";
                break;
            case "SOI":
                strTableName = "tbl_soi";
                serial_branch_num = "branch_num as serial_branch_num";
                break;
            default:
                return false;
        }

        strSQL = @$"SELECT {strTableName}.plant_name,
                           {strTableName}.kinds,
                           {strTableName}.fields,
                           {strTableName}.part_id,
                           {strTableName}.{serial_branch_num},
                           {strTableName}.revisions,
                           {strTableName}.language as english_edition, ";

        if (strTableName == "tbl_wc")
        {
            strSQL = strSQL + @$"{strTableName}.descrip as title,
                                 {strTableName}.descrip_trans as title_trans, ";
        }
        else
        {
            strSQL = strSQL + @$"{strTableName}.title,
                                 {strTableName}.title_trans, ";
        }
        strSQL = strSQL + @$"{strTableName}.editor,
                             {strTableName}.approval_state_pr_rev, ";

        switch (strTableName + ".trans_range")
        {
            case "1":
                strSQL = strSQL + "'部分' as trans_range, ";
                break;
            case "2":
                strSQL = strSQL + "'全文' as trans_range, ";
                break;
            default:
                strSQL = strSQL + "'' as trans_range, ";
                break;
        }

        strSQL = strSQL + @$"{strTableName}.trans_deadline,
                             {strTableName}.trans_lang,
                             {strTableName}.last_update
                             FROM {strTableName}
                             WHERE
                                   {strTableName}.trans_state_req = true AND
                                   {strTableName}.translated_state = false AND
                                   {strTableName}.plant_name = '{gPlantName}' AND
                                   {strTableName}.hold <> 0 AND
                                   {strTableName}.delete_flag = false AND 
                                   ({strTableName}.trans_range = {(int)Enums.TransRange.FULL_TEXT} OR {strTableName}.trans_range = {(int)Enums.TransRange.PART}) AND 
                                    {strTableName}.plant_name = '{findTranslateDto.PlantName}' AND
                                    {strTableName}.kinds = '{findTranslateDto.Kinds}' AND
                                    {strTableName}.fields = '{findTranslateDto.Fields}' AND
                                    {strTableName}.part_id = '{findTranslateDto.PartId}' AND ";
        switch (pTableName.ToUpper())
        {

            case "WC":
            case "OBS":
            case "AFI":
            case "PD":
                strSQL = strSQL + @$"LPAD({strTableName}.serial_num,2,'0') = LPAD('{findTranslateDto.BranchOrSerialNum}',2,'0')";
                break;
            case "GFA":
            case "PFA":
            case "STR":
            case "BP":
            case "SOI":
                strSQL = strSQL + @$"LPAD({strTableName}.branch_num,2,'0') = LPAD('{findTranslateDto.BranchOrSerialNum}',2,'0')";
                break;
        }
        strSQL = strSQL + @$" AND {strTableName}.revisions = '{findTranslateDto.Revisions}'
                              AND {strTableName}.language = '{findTranslateDto.TransLang}' ";
        strSQL = strSQL + @$" AND Not Exists ( SELECT tbl_translate.num
                                               FROM
                                                   vw_translate_control AS tbl_translate
                                               WHERE
                                                     tbl_translate.plant_name = {strTableName}.plant_name AND
                                                     tbl_translate.kinds = {strTableName}.kinds AND
                                                     tbl_translate.fields = {strTableName}.fields AND
                                                     tbl_translate.part_id = {strTableName}.part_id AND ";
        switch (pTableName.ToUpper())
        {
            case "WC":
            case "OBS":
            case "AFI":
            case "PD":
                strSQL = strSQL + @$"tbl_translate.serial_num = {strTableName}.serial_num AND ";
                break;
            case "GFA":
            case "PFA":
            case "STR":
            case "BP":
            case "SOI":
                strSQL = strSQL + @$"tbl_translate.serial_num ={strTableName}.branch_num AND ";
                break;
        }
        strSQL = strSQL + @$"tbl_translate.revisions = {strTableName}.revisions AND
                               (tbl_translate.cancel_status = '' OR tbl_translate.cancel_status is null OR
                                tbl_translate.cancel_status = '{Constants.CANCEL_STATUS_REQUESTING_CANCEL}' OR tbl_translate.cancel_status = '{Constants.CANCEL_STATUS_CONTINUE_TRANS}' ) AND
                               tbl_translate.plant_name = '{findTranslateDto.PlantName}' AND
                               tbl_translate.kinds = '{findTranslateDto.Kinds}' AND
                               tbl_translate.fields = '{findTranslateDto.Fields}' AND
                               tbl_translate.part_id = '{findTranslateDto.PartId}' AND
                               LPAD(tbl_translate.serial_num,2,'0') = LPAD({findTranslateDto.BranchOrSerialNum},2,'0') AND
                               tbl_translate.revisions = '{findTranslateDto.Revisions}' AND ";
        if (findTranslateDto.TransLang == null)
        {
            strSQL = strSQL + @$"tbl_translate.language is null ); ";
        }
        else
        {
            strSQL = strSQL + @$" tbl_translate.language = '{findTranslateDto.TransLang}'); ";
        }

         var translatedb = this.slaveDBContext?.TranslateReponseDtos?
                              .FromSqlRaw(strSQL)
                              .AsEnumerable()
                              .FirstOrDefault();
        if (translatedb != null)
        {
            // 挿入翻訳
            int intMaxNo = 1;
            string serBranNum, lang = string.Empty;
            var item = this.slaveDBContext?.Database.SqlQuery<int>(@$"Select Fn_Get_Max_Offer_Num_Tbl_Tran({findTranslateDto.PlantName});").AsEnumerable();

            if (item != null) intMaxNo = item.ElementAt(0);
            if (translatedb.EnglishEdition == Constants.ENG_LANG) lang = "-E";

            serBranNum = Convert.ToString(translatedb.SerialBranchNum ?? 0);
            string newNum = translatedb.PlantName + "-" + translatedb.Kinds + "-" + translatedb.Fields + "-" + translatedb.PartId + "-" + serBranNum?.PadLeft(2, '0') + "-r" + translatedb.Revisions + lang;

            translatedb.ApprovalStatePrRev = translatedb.ApprovalStatePrRev == null ? 0 : translatedb.ApprovalStatePrRev;
            translatedb.LastUpdate = translatedb.LastUpdate == null ? DateTime.Now : translatedb.LastUpdate;
            string sql = "INSERT INTO tbl_translate";
            sql = sql + " ( ";
            sql = sql + "  num";
            sql = sql + ", english_edition";
            sql = sql + ", offer_num";
            sql = sql + ", title";
            sql = sql + ", title_trans";
            sql = sql + ", editor";
            sql = sql + ", ver_trans_done";
            sql = sql + ", ver_original";
            sql = sql + ", trans_situation";
            sql = sql + ", trans_scope";
            sql = sql + ", trans_deadline";
            sql = sql + ", trans_lang";
            sql = sql + ", trans_req_date";
            sql = sql + ", last_user";
            sql = sql + ",trans_arrival";
            sql = sql + ") VALUES";
            sql = sql + $" (";
            sql = sql + $"  '{newNum}'";
            sql = sql + $", '{translatedb.EnglishEdition}'";
            sql = sql + $", {intMaxNo}";
            sql = sql + $", '{translatedb.Title}'";
            sql = sql + $", '{translatedb.TitleTrans}'";
            sql = sql + $", '{translatedb.Editor}'";
            sql = sql + $", {translatedb.ApprovalStatePrRev}";
            sql = sql + $", '{translatedb.Revisions}'";
            sql = sql + $", '{Constants.NO_OUTPUT}'";
            sql = sql + $", '{translatedb.TransRange}'";
            sql = sql + $", '{translatedb.TransDeadline}'";
            sql = sql + $", '{translatedb.TransLang}'";
            sql = sql + $", '{translatedb.LastUpdate?.ToString("yyyy-MM-dd HH:mm:ss")}'";
            sql = sql + $", '{findTranslateDto.Initial}'";
            sql = sql + $", ''";
            sql = sql + ")";
            var record = this.masterDBContext.Database.ExecuteSqlRaw(sql);
            result = (record > 0);
        }
        else
        {
            result = false;
        }
        return result;
    }

    #endregion
}