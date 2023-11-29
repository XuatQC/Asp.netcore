using FNDSystem.Core;
using FNDSystem.Core.Domain;
using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.Dto;
using FNDSystem.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Office.Interop.Word;
using System.Text;

namespace FNDSystem.Infrastructure.DataAccess.Repositories;
public class PrintRepository : GenericRepository<PrintDataResponeseDto>, IPrintRepository
{
    public PrintRepository(DBMasterContext masterDBContext, DBSlaveContext slaveDBContext) : base(masterDBContext, slaveDBContext)
    {
    }

    #region Public
    #region 印刷データの準備
    /// <summary>
    /// 印刷データの準備
    /// </summary>
    /// <param name="findDto">印刷データ</param>
    /// <returns>印刷データ一覧</returns>
    public IEnumerable<PrintDataResponeseDto>? SetDataListToPrint(FindPrintRequestDto findDto)
    {
        if (findDto.SheetName == null) findDto.SheetName = string.Empty;
        if (findDto.CallForm == null) findDto.CallForm = string.Empty;
        if (findDto.PlantName == null) findDto.PlantName = string.Empty;
        if (findDto.Fields == null) findDto.Fields = string.Empty;
        if (findDto.PartId == null) findDto.PartId = string.Empty;
        if (findDto.EnglishEdition == null) findDto.EnglishEdition = string.Empty;
        if (findDto.ChkTl == null) findDto.ChkTl = false;
        if (findDto.FreeWord == null) findDto.FreeWord = string.Empty;
        var resultList = this.slaveDBContext?.PrintResponses?
                             .FromSql($@"Call Sp_Set_Data_List_To_Print ({findDto.SheetName},
                                                                         {findDto.CallForm},
                                                                         {findDto.PlantName},
                                                                         {findDto.Fields},
                                                                         {findDto.PartId},
                                                                         {findDto.EnglishEdition},
                                                                         {findDto.ChkTl},
                                                                         {findDto.FreeWord});").ToList();

        return resultList;
    }
    #endregion

    #region Word出力処理の準備
    /// <summary>
    /// Word出力処理の準備
    /// </summary>
    /// <param name="findTranslateRequestDto">検索条件</param>
    /// <returns>true: 成功します。 false: 失敗します</returns>
    public List<VwTranslateControl>? PreOutputWord(FindTranslateRequestDto findTranslateRequestDto)
    {
        List<VwTranslateControl>? preTranslateList = null;
        int countRecord = 0;
        //bln処理Flg
        bool blnProcessingFlg;
        var translatedbList = this.slaveDBContext?.VwTranslateControls?.Where(x => x.PlantName == findTranslateRequestDto.PlantName &&
                                                                                     x.Kinds == findTranslateRequestDto.Kinds &&
                                                                                     x.Fields == findTranslateRequestDto.Fields &&
                                                                                     x.PartId == findTranslateRequestDto.PartId &&
                                                                                     x.SerialNum == findTranslateRequestDto.BranchOrSerialNum &&
                                                                                     x.Revisions == findTranslateRequestDto.Revisions &&
                                                                                     x.EnglishEdition == findTranslateRequestDto.TransLang).ToList();

        if (translatedbList != null)
        {
            preTranslateList = new();
            foreach (VwTranslateControl item in translatedbList)
            {
                switch (item.Kinds)
                {
                    case Constants.KIND_WC:

                        break;
                    case Constants.KIND_OBS:

                        countRecord = CountRecordTblObsExtendRefer(item.PlantName + "-" + item.Num);
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

                if (countRecord == 0)
                {
                    blnProcessingFlg = false;
                }
                else
                {
                    blnProcessingFlg = true;
                }

                if (!string.IsNullOrEmpty(item.CancelStatus)) blnProcessingFlg = false;

                item.ProcessingFlg = blnProcessingFlg;

                preTranslateList.Add(item);
            }
        }

        return preTranslateList;
    }
    #endregion

    #region Word出力処理
    /// <summary>
    /// Word出力処理
    /// </summary>
    /// <param name="preTranslateList">事前翻訳リスト</param>
    /// <param name="obsPathsDto">検索条件</param>
    public bool ProcessingOutputWord(string inLang, List<VwTranslateControl> preTranslateList, ObsPathsDto obsPathsDto)
    {
        bool result = false;
        string strProcessingResult = string.Empty;
        string strProcessingResult2 = string.Empty;
        string stringVal = string.Empty;
        string strSQL = string.Empty;
        string userInitials = string.Empty;
        StringBuilder sbd;
        foreach (VwTranslateControl item in preTranslateList)
        {
            if (item.ProcessingFlg)
            {
                strProcessingResult = "9";
                switch (item.Kinds)
                {
                    case Constants.KIND_WC:
                        strProcessingResult = "tbl_WC(past)";
                        break;
                    case Constants.KIND_OBS:
                        strProcessingResult = this.OutputOBSTrans(item.OfferNum.ToString(), inLang, obsPathsDto);
                        break;
                    case Constants.KIND_GFA:
                        strProcessingResult = "Sp_Get_Tbl_Gfa_Extend_Refer";
                        break;
                    case Constants.KIND_PFA:
                        strProcessingResult = "Sp_Get_Tbl_Pfa_Extend_Refer";
                        break;
                    case Constants.KIND_AFI:
                        strProcessingResult = "Sp_Get_Tbl_Afi_Extend_Refer";
                        break;
                    case Constants.KIND_PD:
                        strProcessingResult = "Sp_Get_Tbl_Pd_Extend_Refer";
                        break;
                    case Constants.KIND_STR:
                        strProcessingResult = "Sp_Get_Tbl_Str_Extend_Refer";
                        break;
                    case Constants.KIND_BP:
                        strProcessingResult = "Sp_Get_Tbl_Bp_Extend_Refer";
                        break;
                    case Constants.KIND_SOI:
                        strProcessingResult = "Sp_Get_Tbl_Soi_Extend_Refer";
                        break;
                }

                if (strProcessingResult.Substring(0, 1) == "0")
                {
                    strProcessingResult2 = CheckDBWordFile(item.Kinds, item.OfferNum.ToString(), strProcessingResult.Substring(0, 2));
                    if (strProcessingResult2 == "0")
                    {
                        stringVal = obsPathsDto.DOCS_PUT_LOCAL + strProcessingResult.Substring(0, 2);
                        if (Path.GetFileName(stringVal) == string.Empty)
                        {
                            strProcessingResult2 = "1";
                        }
                    }
                }

                if ((strProcessingResult.Substring(0, 1) == "0" && strProcessingResult2 == "0") &&
                    (item.TransSituation != Constants.TRANS_SITUATION_PRINTED || item.OutputDate != DateTime.Now))
                {
                    sbd = new StringBuilder();
                    //'WORDファイル格納情報ファイルを更新
                    sbd.Append("UPDATE tbl_translate SET ");
                    sbd.Append("trans_situation = ");
                    sbd.Append("'");
                    sbd.Append(Constants.TRANS_SITUATION_PRINTED);
                    sbd.Append("'");
                    if (strProcessingResult.Length > 1)
                    {
                        sbd.Append(",file_name = ");
                        sbd.Append("'");
                        sbd.Append(strProcessingResult.Substring(0, 2));
                        sbd.Append("'");
                    }
                    sbd.Append(",output_date = ");
                    sbd.Append("'");
                    sbd.Append(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    sbd.Append("'");
                    sbd.Append(",last_update = ");
                    sbd.Append("'");
                    sbd.Append(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    sbd.Append("'");
                    sbd.Append(",last_user = ");
                    sbd.Append("'");
                    sbd.Append(userInitials);
                    sbd.Append("'");
                    sbd.Append("WHERE offer_num = ");
                    sbd.Append("'");
                    sbd.Append(item.OfferNum);
                    sbd.Append("';");
                    strSQL = sbd.ToString();
                    this.masterDBContext?.Database.ExecuteSqlRaw(strSQL);
                }
            }
        }
        return result;
    }
    #endregion

    #region Input

    #region OBS 翻訳済Word文書の取込
    /// <summary>
    /// OBS 翻訳済Word文書の取込
    /// </summary>
    /// <param name="procesDetail"></param>
    /// <param name="offerNum"></param>
    /// <param name="lNum"></param>
    /// <param name="pSyncpath"></param>
    /// <param name="pTable"></param>
    /// <param name="strDocsInTrans"></param>
    /// <returns></returns>
    public string InputOBSTrans(string procesDetail, int offerNum, string lNum, string pSyncpath, string? pTable, string strDocsInTrans)
    {

        string strInputOBSTrans = string.Empty;
        string keyNum = string.Empty;
        string keyDOCX = string.Empty;

        //str英語版
        string strEnglishEdition = string.Empty;
        //intリビジョン
        int? revisions = 0;
        int i = 0;
        int intRowsCnt = 0;
        //vnt原文
        string[]? vntOriginal = null;
        string[]? vntSEQNo = null;
        //str訳文
        string strTrans = string.Empty;

        //outタイトル
        string? outTitle = string.Empty;
        //bln事実Upd
        bool blnFactsUpd = false;
        //out事実(40)
        string[] outFacts = new string[40];
        //out事実SEQNo(40)
        string[]? outFactsSEQNo = null;
        //cnt事実
        int cntFacts;

        //bln結論Upd
        bool blnConclusionUpd = false;
        //out結論(10)
        string[] outConclusion = new string[10];
        //out結論SEQNo(10)
        string[]? outConclusionSEQNo = null;
        int cntConclusion;

        //str翻訳言語
        string strTransLang = string.Empty;

        #region processing tbl_translate
        strInputOBSTrans = "0" + Constants.DELIMITER_CHARACTER + Constants.DELIMITER_CHARACTER;

        cntConclusion = 0;
        cntFacts = 0;

        if (pTable?.ToUpper() == "tbl_translate".ToUpper())
        {
            VwTranslateControl? translate = null;
            translate = this.slaveDBContext?.VwTranslateControls?.Where(x => x.OfferNum == offerNum).FirstOrDefault();

            if (translate != null)
            {
                keyDOCX = translate.FileName == null ? string.Empty : translate.FileName;       // Nz(rsL("ファイル名")) // file_name
                revisions = translate.Revisions == null ? 0 : translate.Revisions;      // intリビジョン = rsL("リビジョン") // revisions
                strEnglishEdition = translate.EnglishEdition == null ? string.Empty : translate.EnglishEdition;     //str英語版 = Nz(rsL("英語版"))   // english_edition
                strTransLang = translate.TransLang == null ? string.Empty : translate.TransLang;    //str翻訳言語 = Nz(rsL("翻訳言語"))   // trans_lang
            }
            else
            {
                strInputOBSTrans = "1" + Constants.DELIMITER_CHARACTER + Constants.DELIMITER_CHARACTER; // "1@@@@@@"
                return strInputOBSTrans;
            }
        }
        else//wkt_Translate
        {
            WktTranslate? wktTranslate = null;
            wktTranslate = this.slaveDBContext?.WktTranslates?.Where(x => x.OfferNum == offerNum).FirstOrDefault();

            if (wktTranslate != null)
            {
                keyDOCX = wktTranslate.FileName == null ? string.Empty : wktTranslate.FileName;       // Nz(rsL("ファイル名")) // file_name
                revisions = wktTranslate.Revisions == null ? 0 : wktTranslate.Revisions;      // intリビジョン = rsL("リビジョン") // revisions
                strEnglishEdition = wktTranslate.EnglishEdition == null ? string.Empty : wktTranslate.EnglishEdition;     //str英語版 = Nz(rsL("英語版"))   // english_edition
                strTransLang = wktTranslate.TransLang == null ? string.Empty : wktTranslate.TransLang;    //str翻訳言語 = Nz(rsL("翻訳言語"))   // trans_lang
            }
            else
            {
                strInputOBSTrans = "1" + Constants.DELIMITER_CHARACTER + Constants.DELIMITER_CHARACTER; // "1@@@@@@"
                return strInputOBSTrans;
            }

        }

        keyNum = lNum;
        // read word file
        // g_strDOCS_IN_TRANS: lấy từ màn welcome
        Application wordApp = new Application();

        string titleWordVal = string.Empty;
        string scopeVal = string.Empty;
        string strSQL = string.Empty;
        #endregion

        #region processing tbl_obs_scope
        for (int index = 0; index < intRowsCnt - 3; index++)
        {
            titleWordVal = wordApp.ActiveDocument.Tables[1].Cell(index + 3, 2).Range.Text.Substring(0, 2);
            switch (titleWordVal)
            {
                case Constants.TITLE:
                    if (strTransLang == Constants.JAP_LANG)
                    {
                        if (wordApp.ActiveDocument.Tables[1].Cell(index + 3, 3).Range.Text.Length > 0)
                        {
                            outTitle = StringUtils.PastTransSentence(wordApp, i + 3, 3);
                        }
                    }
                    else
                    {
                        if (wordApp.ActiveDocument.Tables[1].Cell(index + 3, 4).Range.Text.Length > 0)
                        {
                            outTitle = StringUtils.PastTransSentence(wordApp, i + 3, 4);
                        }
                    }
                    break;
                case Constants.RANGE:
                    if ((procesDetail == Constants.PROCESS_DETAIL_CAPTURE || procesDetail == Constants.PROCESS_DETAIL_UPDATE) && (strTransLang == Constants.JAP_LANG))
                    {
                        if (wordApp.ActiveDocument.Tables[1].Cell(index + 3, 3).Range.Text.Length > 0)
                        {
                            scopeVal = wordApp.ActiveDocument.Tables[1].Cell(index + 3, 3).Range.Text.Trim();
                            strSQL = @$"UPDATE tbl_obs_scope SET
                                            scope = '{scopeVal}'
                                        WHERE num = '{keyNum}' ;";
                            this.masterDBContext?.Database.ExecuteSqlRaw(strSQL);
                        }
                        else
                        {
                            if (wordApp.ActiveDocument.Tables[1].Cell(index + 3, 4).Range.Text.Length > 0)
                            {
                                scopeVal = wordApp.ActiveDocument.Tables[1].Cell(index + 3, 4).Range.Text.Trim();
                                strSQL = @$"UPDATE tbl_obs_scope SET
                                                scope_trans = '{scopeVal}'
                                            WHERE num = '{keyNum}' ;";
                                this.masterDBContext?.Database.ExecuteSqlRaw(strSQL);
                            }
                        }
                    }
                    break;
                case Constants.FACT:
                    if (strTransLang == Constants.JAP_LANG)
                    {
                        if (wordApp.ActiveDocument.Tables[1].Cell(index + 3, 3).Range.Text.Length > 0)
                        {
                            blnFactsUpd = true;
                            outFacts[cntFacts] = StringUtils.ResCharConPro(wordApp.ActiveDocument.Tables[1].Cell(index + 3, 3).Range.Text);
                            if (outFactsSEQNo != null) outFactsSEQNo[cntFacts] = wordApp.ActiveDocument.Tables[1].Cell(index + 3, 2).Range.Text.Substring(3, 3);
                            cntFacts = cntFacts + 1;
                        }
                        else
                        {
                            blnFactsUpd = true;
                            outFacts[cntFacts] = StringUtils.ResCharConPro(wordApp.ActiveDocument.Tables[1].Cell(index + 3, 4).Range.Text);
                            if (outFactsSEQNo != null) outFactsSEQNo[cntFacts] = wordApp.ActiveDocument.Tables[1].Cell(index + 3, 2).Range.Text.Substring(3, 3);
                            cntFacts = cntFacts + 1;
                        }
                    }
                    break;
                case Constants.CONCLUSION:
                    if ((procesDetail == Constants.PROCESS_DETAIL_CAPTURE || procesDetail == Constants.PROCESS_DETAIL_UPDATE) && (strTransLang == Constants.JAP_LANG))
                    {
                        if (wordApp.ActiveDocument.Tables[1].Cell(index + 3, 3).Range.Text.Length > 0)
                        {
                            blnFactsUpd = true;
                            outFacts[cntFacts] = StringUtils.ResCharConPro(wordApp.ActiveDocument.Tables[1].Cell(index + 3, 3).Range.Text);
                            if (outFactsSEQNo != null) outFactsSEQNo[cntFacts] = wordApp.ActiveDocument.Tables[1].Cell(index + 3, 2).Range.Text.Substring(3, 3);
                            cntFacts = cntFacts + 1;

                        }
                        else
                        {
                            if (wordApp.ActiveDocument.Tables[1].Cell(index + 3, 4).Range.Text.Length > 0)
                            {
                                blnFactsUpd = true;
                                outFacts[cntFacts] = StringUtils.ResCharConPro(wordApp.ActiveDocument.Tables[1].Cell(index + 3, 4).Range.Text);
                                if (outFactsSEQNo != null) outFactsSEQNo[cntFacts] = wordApp.ActiveDocument.Tables[1].Cell(index + 3, 2).Range.Text.Substring(3, 3);
                                cntFacts = cntFacts + 1;
                            }
                        }
                    }
                    break;
                default:
                    // code block
                    break;
            }
        }
        #endregion

        #region processing tbl_OBS_Fact を更新（事実）
        if (blnFactsUpd)
        {
            ObsFact? obsFact = new();
            obsFact = this.slaveDBContext?.ObsFacts?.Where(x => x.Num == keyNum).FirstOrDefault();

            if (obsFact != null)
            {
                if (strTransLang == Constants.JAP_LANG)
                {
                    vntOriginal = obsFact?.Fact?.Split(Constants.DELIMITER_CHARACTER);
                }
                else
                {
                    vntOriginal = obsFact?.FactTrans?.Split(Constants.DELIMITER_CHARACTER);
                }
            }
            else
            {

                if (strTransLang == Constants.JAP_LANG)
                {
                    vntOriginal = new string[] { obsFact?.Fact == null ? string.Empty : obsFact.Fact };
                }
                else
                {
                    vntOriginal = new string[] { obsFact?.FactTrans == null ? string.Empty : obsFact.FactTrans };
                }
            }

            strTrans = string.Empty;
            blnFactsUpd = false;
            StringBuilder bld = new StringBuilder();
            for (int indx1 = 0; indx1 < vntSEQNo?.Length; indx1++)
            {
                for (int indx2 = 0; indx2 < cntFacts - 1; indx2++)
                {
                    if (!string.IsNullOrEmpty(outFacts?[indx2]) && (outFactsSEQNo != null && outFactsSEQNo[indx2] == vntSEQNo?[indx1]))
                    {
                        if (vntOriginal?.Length >= indx1)
                        {
                            vntOriginal[indx1] = "<div>" + outFacts[indx2] + "</div>";
                            blnFactsUpd = true;
                        }
                        else if (vntOriginal?.Length == 0 && vntSEQNo.Length == 0)
                        {
                            bld.Append("<div>" + outFacts[indx2] + "</div>");
                            blnFactsUpd = true;
                        }
                    }
                }


                for (indx1 = 0; indx1 < vntOriginal?.Length; indx1++)
                {
                    if (indx1 > 0)
                    {
                        bld.Append(Constants.DELIMITER_CHARACTER);

                    }
                    bld.Append(vntOriginal[indx1]);
                }
                strTrans = bld.ToString();
            }

            if (blnFactsUpd && (procesDetail == Constants.PROCESS_DETAIL_CAPTURE || procesDetail == Constants.PROCESS_DETAIL_UPDATE))
            {
                strSQL = @$"UPDATE tbl_obs_past SET ";

                if (strTransLang == Constants.JAP_LANG)
                {
                    strSQL += @$"fact = '{strTrans}'";
                }
                else
                {
                    strSQL += @$",fact_trans = '{strTrans}'";
                }
                strSQL += @$",last_update = '{DateTime.Now}' WHERE num = '{keyNum}' ;";
                this.masterDBContext?.Database.ExecuteSqlRaw(strSQL);
            }
        }
        #endregion

        #region processing tbl_OBS_Conclusion を更新（結論）
        if (blnConclusionUpd)
        {
            ObsConclusion? obsConclusion = new();
            obsConclusion = this.slaveDBContext?.ObsConclusions?.Where(x => x.Num == keyNum).FirstOrDefault();
            if (obsConclusion != null)
            {
                if (strTransLang == Constants.JAP_LANG)
                {
                    vntOriginal = obsConclusion?.Conclusion?.Split(Constants.DELIMITER_CHARACTER);
                }
                else
                {
                    vntOriginal = obsConclusion?.ConclusionTrans?.Split(Constants.DELIMITER_CHARACTER);
                }
            }
            else
            {
                if (strTransLang == Constants.JAP_LANG)
                {
                    vntOriginal = new string[] { obsConclusion?.Conclusion == null ? string.Empty : obsConclusion.Conclusion };
                }
                else
                {
                    vntOriginal = new string[] { obsConclusion?.ConclusionTrans == null ? string.Empty : obsConclusion.ConclusionTrans };
                }
                string strSeqNo = string.Empty;
                if (obsConclusion != null && obsConclusion.ConclusionNum > 0)
                {
                    // TO DO
                }

                vntSEQNo = new string[] { strSeqNo };
            }

            blnConclusionUpd = false;
            StringBuilder bld = new StringBuilder();
            for (int indx1 = 0; indx1 < vntSEQNo?.Length; indx1++)
            {
                for (int indx2 = 0; indx2 < cntConclusion - 1; indx2++)
                {
                    if (!string.IsNullOrEmpty(outConclusion[indx2]) && (outConclusionSEQNo != null && outConclusionSEQNo[indx2] == vntSEQNo?[indx1]))
                    {
                        if (vntOriginal?.Length >= indx1)
                        {
                            vntOriginal[indx1] = "<div>" + outConclusion[indx2] + "</div>";
                            blnConclusionUpd = true;
                        }
                        else if (vntOriginal?.Length == 0 && vntSEQNo.Length == 0)
                        {
                            bld.Append("<div>" + outConclusion[indx2] + "</div>");
                            blnConclusionUpd = true;
                        }
                    }
                }
            }

            for (int indx1 = 0; indx1 < vntOriginal?.Length; indx1++)
            {
                if (indx1 > 0)
                {
                    bld.Append(Constants.DELIMITER_CHARACTER);
                }
                bld.Append(vntOriginal[indx1]);
            }
            strTrans = bld.ToString();

            if (blnConclusionUpd && (procesDetail == Constants.PROCESS_DETAIL_CAPTURE || procesDetail == Constants.PROCESS_DETAIL_UPDATE))
            {
                strSQL = @$"UPDATE tbl_obs_conclusion SET ";

                if (strTransLang == Constants.JAP_LANG)
                {
                    strSQL += @$"conclusion = '{strTrans}'";
                }
                else
                {
                    strSQL += @$",conclusion_trans = '{strTrans}'";
                }
                strSQL += @$",last_update = '{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}' WHERE num = '{keyNum}' ;";
                this.masterDBContext?.Database.ExecuteSqlRaw(strSQL);
            }
        }
        #endregion

        #region processing tbl_OBS を更新（タイトル、翻訳状況など）
        strSQL = @$"UPDATE tbl_obs SET ";
        strSQL += @$"approval_state_pr_rev = {revisions}";
        if ((procesDetail == Constants.PROCESS_DETAIL_CAPTURE || procesDetail == Constants.PROCESS_DETAIL_UPDATE) && string.IsNullOrEmpty(outTitle))
        {
            if (strTransLang == Constants.JAP_LANG)
            {
                strSQL += @$", title = '{outTitle}'";
            }
            else
            {
                strSQL += @$", title_trans = '{outTitle}'";
            }
        }

        if (procesDetail == Constants.PROCESS_DETAIL_CAPTURE)
        {
            strSQL += @$", translated_state = true";
        }

        strSQL += @$",last_update = '{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}' WHERE ";
        strSQL += @$" CONCAT(plant_name,'-',kinds,'-','fields','-',part_id,LPAD(serial_num,2,'0'),'-r',revisions,IF((`tbl_obs`.`english_edition` = 'E'),'-E','')) = '{keyNum}' ;";
        this.masterDBContext?.Database.ExecuteSqlRaw(strSQL);
        #endregion

        return strInputOBSTrans;
    }
    #endregion

    #endregion

    #region OutPut

    #region OBS 翻訳用Word文書の出力
    /// <summary>
    /// OBS 翻訳用Word文書の出力
    /// p依頼番号: offer_num
    /// </summary>
    /// <returns></returns>
    public string OutputOBSTrans(string? offerNum, string? lang, ObsPathsDto obsPathsDto)
    {
        string strOutputOBSTrans = string.Empty;
        string? keyNum = string.Empty;
        string outOriginal = string.Empty;

        int cnt = 0;
        //str現プラント名
        string? strCurrentPlantName = string.Empty;
        //str現種類
        string? strCurrentKind = string.Empty;
        //str現分野
        string? strCurrentField = string.Empty;
        //str現個別ID
        string? strCurrentPartID = string.Empty;
        //int現通番
        int intCurrentSerialNumber = 0;
        //int現リビジョン
        int intCurrentRevision = 0;
        //str現英語版
        string? strCurrentEnglishEdition = string.Empty;
        //int現翻訳範囲
        int intCurrentTransRange = 0;
        //bln翻訳状況翻訳済
        bool blnTranslatedState = false;
        //str承認状況レビュワ_リビジョン
        int? strApprovalStatePrRev = 0;

        //ary現SEQNo
        string[] aryCurrentSEQNo = new string[] { };
        //ary現和文
        string[] aryInJapanese = new string[] { };
        //ary現英文
        string[] aryInEnglish = new string[] { };
        //ary現部分翻訳
        string[] aryCurrentPartTrans = new string[] { };
        int i = 0;

        string[] aryOldSEQNo = new string[] { };
        string[] aryOldJapanese = new string[] { };
        string[] aryOldEnglish = new string[] { };
        int j = 0;

        bool blnOldExist = false;
        bool blnChildOldExist = false;
        bool blnChildNewExist = false;
        string outTranslation = string.Empty;

        int lngCharCnt = 0;

        //str希望期限
        string? strTransDeadline = string.Empty;
        //str翻訳言語
        string? strTransLang = string.Empty;
        string strDocFileName = string.Empty;
        string strOldTranslator = string.Empty;
        bool blnChildOldExistS = false;

        strOutputOBSTrans = "0";
        #region wkt_Translate テーブル
        WktTranslate? wktTranslate = new WktTranslate();
        wktTranslate = this.slaveDBContext?.WktTranslates?.Where(x => x.OfferNum == Convert.ToInt32(offerNum)).FirstOrDefault();
        if (wktTranslate != null && wktTranslate.Id > 0)
        {
            keyNum = wktTranslate.PlantName + "-" + wktTranslate.Num;
            strTransDeadline = wktTranslate.TransDeadline;
            strTransLang = wktTranslate.TransLang;
        }
        else
        {
            strOutputOBSTrans = "1";
        }

        cnt = 0;
        intCurrentTransRange = 0;
        strOldTranslator = string.Empty;

        List<ObsExtendReferDto>? obsExtendReferList = this.slaveDBContext?.ObsExtendRefers?
                                                          .FromSql($@"Select * from vw_obs_extend_refer;")
                                                          .ToList();
        ObsExtendReferDto? obsExtendReferDto = obsExtendReferList?.Where(x => x.Num == keyNum).ToList()?.FirstOrDefault();

        if (obsExtendReferDto != null)
        {
            strCurrentPlantName = obsExtendReferDto.PlantName;
            strCurrentKind = obsExtendReferDto.Kinds;
            strCurrentField = obsExtendReferDto.Fields;
            strCurrentPartID = obsExtendReferDto.PartId;
            intCurrentSerialNumber = Convert.ToInt32(obsExtendReferDto.SerialNum);
            intCurrentRevision = Convert.ToInt32(obsExtendReferDto.Revisions);
            strCurrentEnglishEdition = obsExtendReferDto.Language != null ? obsExtendReferDto.Language : string.Empty;
            intCurrentTransRange = Convert.ToInt32(obsExtendReferDto.TransRange);
            strApprovalStatePrRev = obsExtendReferDto.ApprovalStatePrRev;
        }
        else
        {
            strOutputOBSTrans = "1";
        }

        ObsExtendReferDto? obsExtendReferDtoOld = obsExtendReferList?.Where(x => x.PlantName == strCurrentPlantName &&
                                                                                 x.Kinds == strCurrentKind &&
                                                                                 x.Fields == strCurrentField &&
                                                                                 x.PartId == strCurrentPartID &&
                                                                                 x.SerialNum == intCurrentSerialNumber &&
                                                                                 x.Revisions < intCurrentRevision &&
                                                                                 x.TranslatedState).OrderByDescending(x => x.Revisions).FirstOrDefault();
        if (obsExtendReferDtoOld != null)
        {
            blnOldExist = true;
            strOldTranslator = L_getTranslator(obsExtendReferDtoOld.Num);
        }

        #region TODO

        // 現・旧バージョン比較用Wordファイルを開く


        #endregion

        //'■タイトルを出力
        if (intCurrentTransRange == (int)Enums.TransRange.FULL_TEXT ||
           (intCurrentTransRange == (int)Enums.TransRange.PART && obsExtendReferDtoOld != null && obsExtendReferDtoOld.PartTrans == true))
        {
            cnt = cnt + 1;

            #region TODO

            #endregion

            //' 旧バージョン用Wordファイルを編集・保存
            if (blnOldExist)
            {
                if (strTransLang == Constants.JAP_LANG)
                {
                    if (obsExtendReferDtoOld != null && obsExtendReferDtoOld.TitleTrans != null) outOriginal = obsExtendReferDtoOld.TitleTrans;
                    if (obsExtendReferDtoOld != null && obsExtendReferDtoOld.Title != null) outTranslation = obsExtendReferDtoOld.Title;
                }
                else
                {
                    if (obsExtendReferDtoOld != null && obsExtendReferDtoOld.Title != null) outOriginal = obsExtendReferDtoOld.Title;
                    if (obsExtendReferDtoOld != null && obsExtendReferDtoOld.TitleTrans != null) outTranslation = obsExtendReferDtoOld.TitleTrans;
                }
            }
            else
            {
                outOriginal = string.Empty;
                outTranslation = string.Empty;
            }

            if (blnTranslatedState || strApprovalStatePrRev > 0)
            {
                if (strTransLang == Constants.JAP_LANG)
                {
                    if (obsExtendReferDtoOld != null && obsExtendReferDtoOld.Title != null) outTranslation = obsExtendReferDtoOld.Title;
                }
                else
                {
                    if (obsExtendReferDtoOld != null && obsExtendReferDtoOld.TitleTrans != null) outTranslation = obsExtendReferDtoOld.TitleTrans;
                }
            }

            //' 現バージョン用Wordファイルを編集・保存
            if (strTransLang == Constants.JAP_LANG)
            {
                if (obsExtendReferDtoOld != null && obsExtendReferDtoOld.TitleTrans != null) outOriginal = obsExtendReferDtoOld.TitleTrans;
            }
            else
            {
                if (obsExtendReferDtoOld != null && obsExtendReferDtoOld.Title != null) outOriginal = obsExtendReferDtoOld.Title;
            }

            if (!blnOldExist)
            {
                if (strTransLang == Constants.JAP_LANG)
                {
                    if (obsExtendReferDtoOld != null && obsExtendReferDtoOld.Title != null) outTranslation = obsExtendReferDtoOld.Title;
                }
                else
                {
                    if (obsExtendReferDtoOld != null && obsExtendReferDtoOld.TitleTrans != null) outTranslation = obsExtendReferDtoOld.TitleTrans;
                }
            }

            #region TODO L_TempFile_EditSave

            #endregion

            lngCharCnt = lngCharCnt + outOriginal.Length;
        }

        #endregion

        //範囲を出力

        //事実を出力
        #region 旧 tbl_OBS_Fact
        //' 現 tbl_OBS_Fact
        ObsFact? obsFactDB = new();
        obsFactDB = this.slaveDBContext?.ObsFacts?.Where(x => x.Num == keyNum).FirstOrDefault();
        if (obsFactDB != null)
        {
            blnChildNewExist = true;
            if (obsFactDB.Fact != null) aryInJapanese = new string[] { obsFactDB.Fact };
            if (obsFactDB.FactTrans != null) aryInEnglish = new string[] { obsFactDB.FactTrans };
        }
        else
        {
            blnChildNewExist = false;
        }


        ObsExtendedReferObsFactDto? oldObsExtendedReferObsFactDto = new();
        oldObsExtendedReferObsFactDto = this.slaveDBContext?.ObsExtendedReferObsFactDtos?
                                            .FromSql($@"Call Sp_Get_Obs_Extend_Refer_Obs_Fact(  {strCurrentPlantName}
                                                                                                ,{strCurrentKind}
                                                                                                ,{strCurrentField}
                                                                                                ,{strCurrentPartID}
                                                                                                ,{intCurrentSerialNumber}
                                                                                                ,{intCurrentRevision});")
                                            .AsEnumerable().FirstOrDefault();

        if (oldObsExtendedReferObsFactDto != null)
        {
            if (oldObsExtendedReferObsFactDto.SeqNum != null) aryCurrentSEQNo = new string[] { oldObsExtendedReferObsFactDto.SeqNum };
            if (oldObsExtendedReferObsFactDto.Fact != null) aryInJapanese = new string[] { oldObsExtendedReferObsFactDto.Fact };
            if (oldObsExtendedReferObsFactDto.FactTrans != null) aryInEnglish = new string[] { oldObsExtendedReferObsFactDto.FactTrans };
        }

        if ((obsFactDB != null) &&
        (intCurrentTransRange == (int)Enums.TransRange.FULL_TEXT ||
               (intCurrentTransRange == (int)Enums.TransRange.PART && Convert.ToBoolean(aryCurrentPartTrans[i]))))
        {
            cnt = cnt + 1;
            #region TODO
            ////' 行挿入
            #endregion

            blnChildOldExistS = false;
            if (blnChildOldExist)
            {
                for (j = 0; j < oldObsExtendedReferObsFactDto?.Quantity; j++)
                {
                    if (aryCurrentSEQNo[i] == aryOldSEQNo[j])
                    {
                        if (strTransLang == Constants.JAP_LANG)
                        {
                            outOriginal = aryOldEnglish[j] != null ? aryOldEnglish[j] : string.Empty;
                            outTranslation = aryOldJapanese[j] != null ? aryOldJapanese[j] : string.Empty;
                        }
                        else
                        {
                            outOriginal = aryOldJapanese[j] != null ? aryOldJapanese[j] : string.Empty;
                            outTranslation = aryOldEnglish[j] != null ? aryOldEnglish[j] : string.Empty;
                        }
                        blnChildOldExistS = true;
                        break;
                    }
                }
            }

            if (blnChildNewExist && (blnTranslatedState || strApprovalStatePrRev > 0))
            {
                if (strTransLang == Constants.JAP_LANG)
                {
                    outTranslation = aryOldJapanese[i];
                }
                else
                {
                    outTranslation = aryOldEnglish[i];
                }
            }

            //現バージョン用Wordファイルを編集・保存
            if (blnChildNewExist)
            {
                if (strTransLang == Constants.JAP_LANG)
                {
                    if (aryOldEnglish.Length > i) outOriginal = aryOldEnglish[i];
                }
                else
                {
                    if (aryOldJapanese.Length > i) outOriginal = aryOldJapanese[i];
                }

                if (!blnChildOldExist || !blnChildOldExistS)
                {
                    if (strTransLang == Constants.JAP_LANG)
                    {
                        if (aryInJapanese.Length > i) outTranslation = aryInJapanese[i] != null ? aryInJapanese[i] : string.Empty;
                    }
                    else
                    {
                        if (aryInEnglish.Length > i) outTranslation = aryInEnglish[i] != null ? aryInEnglish[i] : string.Empty;
                    }
                }
            }
            else
            {
                outOriginal = string.Empty;
            }

            lngCharCnt = lngCharCnt + outOriginal.Length;

        }
        #endregion

        // 結論を出力
        #region tbl_OBS_Conclusion
        //現 tbl_OBS_Conclusion
        ObsConclusion? obsConclusionDB = new();
        obsConclusionDB = this.slaveDBContext?.ObsConclusions?.Where(x => x.Num == keyNum).FirstOrDefault();
        if (obsConclusionDB != null)
        {
            blnChildNewExist = true;
            if (obsConclusionDB.Conclusion != null) aryInJapanese = new string[] { obsConclusionDB.Conclusion };
            if (obsConclusionDB.ConclusionTrans != null) aryInEnglish = new string[] { obsConclusionDB.ConclusionTrans };
        }
        else
        {
            blnChildNewExist = false;
        }


        ObsExtendedReferObsConclusionDto? oldObsExtendedReferObsConclusionDto = new();
        oldObsExtendedReferObsConclusionDto = this.slaveDBContext?.ObsExtendedReferObsConclusionDtos?
                                                  .FromSql($@"Call Sp_Get_Obs_Extend_Refer_Obs_Conclusion({strCurrentPlantName}
                                                                                                            ,{strCurrentKind}
                                                                                                            ,{strCurrentField}
                                                                                                            ,{strCurrentPartID}
                                                                                                            ,{intCurrentSerialNumber}
                                                                                                            ,{intCurrentRevision});")
                                                  .AsEnumerable().FirstOrDefault();
        if (oldObsExtendedReferObsConclusionDto != null)
        {
            if (obsConclusionDB != null)
            {
                if (oldObsExtendedReferObsConclusionDto.SeqNum != null) aryCurrentSEQNo = oldObsExtendedReferObsConclusionDto.SeqNum.Split(Constants.DELIMITER_NUMBER);
                if (oldObsExtendedReferObsConclusionDto.Conclusion != null) aryInJapanese = oldObsExtendedReferObsConclusionDto.Conclusion.Split(Constants.DELIMITER_CHARACTER);
                if (oldObsExtendedReferObsConclusionDto.ConclusionTrans != null) aryInEnglish = oldObsExtendedReferObsConclusionDto.ConclusionTrans.Split(Constants.DELIMITER_CHARACTER);
            }
            else
            {
                if (oldObsExtendedReferObsConclusionDto.SeqNum != null) aryCurrentSEQNo = new string[] { oldObsExtendedReferObsConclusionDto.SeqNum };
                if (oldObsExtendedReferObsConclusionDto.Conclusion != null) aryInJapanese = new string[] { oldObsExtendedReferObsConclusionDto.Conclusion };
                if (oldObsExtendedReferObsConclusionDto.ConclusionTrans != null) aryInEnglish = new string[] { oldObsExtendedReferObsConclusionDto.ConclusionTrans };
            }
        }

        if (obsConclusionDB != null)
        {
            for (i = 0; i < 10; i++)
            {
                if (intCurrentTransRange == (int)Enums.TransRange.FULL_TEXT ||
                   (intCurrentTransRange == (int)Enums.TransRange.PART && Convert.ToBoolean(aryCurrentPartTrans[i])))
                {
                    cnt = cnt + 1;

                    //旧バージョン用Wordファイルを編集・保存
                    outOriginal = string.Empty;
                    outTranslation = string.Empty;

                    blnChildOldExistS = false;
                    if (blnChildOldExist)
                    {
                        for (j = 0; j < oldObsExtendedReferObsConclusionDto?.Quantity; j++)
                        {
                            if (aryCurrentSEQNo[i] == aryOldSEQNo[j])
                            {
                                if (strTransLang == Constants.JAP_LANG)
                                {
                                    if (aryOldEnglish.Length > j) outOriginal = aryOldEnglish[j] != null ? aryOldEnglish[j] : string.Empty;
                                    if (aryOldJapanese.Length > j) outTranslation = aryOldJapanese[j] != null ? aryOldJapanese[j] : string.Empty;
                                }
                                else
                                {
                                    if (aryOldJapanese.Length > j) outOriginal = aryOldJapanese[j] != null ? aryOldJapanese[j] : string.Empty;
                                    if (aryOldEnglish.Length > j) outTranslation = aryOldEnglish[j] != null ? aryOldEnglish[j] : string.Empty;
                                }
                                blnChildOldExistS = true;
                                break;
                            }
                        }
                    }

                    if (blnChildNewExist && (blnTranslatedState || strApprovalStatePrRev > 0))
                    {
                        if (strTransLang == Constants.JAP_LANG)
                        {
                            if (aryOldJapanese.Length > i) outTranslation = aryOldJapanese[i];
                        }
                        else
                        {
                            if (aryOldEnglish.Length > i) outTranslation = aryOldEnglish[i];
                        }
                    }

                    //現バージョン用Wordファイルを編集・保存
                    if (blnChildNewExist)
                    {
                        if (strTransLang == Constants.JAP_LANG)
                        {
                            if (aryOldEnglish.Length > i) outOriginal = aryOldEnglish[i];
                        }
                        else
                        {
                            if (aryOldJapanese.Length > i) outOriginal = aryOldJapanese[i];
                        }

                        if (!blnChildOldExist || !blnChildOldExistS)
                        {
                            if (strTransLang == Constants.JAP_LANG)
                            {
                                if (aryInJapanese.Length > i) outTranslation = aryInJapanese[i] != null ? aryInJapanese[i] : string.Empty;
                            }
                            else
                            {
                                if (aryInEnglish.Length > i) outTranslation = aryInEnglish[i] != null ? aryInEnglish[i] : string.Empty;
                            }
                        }
                    }
                    else
                    {
                        outOriginal = string.Empty;
                    }
                    #region Todo L_TempFile_EditSave

                    #endregion
                    lngCharCnt = lngCharCnt + outOriginal.Length;
                }
            }
        }
        #endregion

        #region TODO for Word
        //' Word の保存

        if (!string.IsNullOrEmpty(strOldTranslator))
        {
            strOldTranslator = "." + strOldTranslator;
        }
        else
        {
            strOldTranslator = string.Empty;
        }
        strDocFileName = keyNum + "（" + lngCharCnt + Constants.LETTER + strTransDeadline + strOldTranslator + "）" + ".docx";
        #endregion

        //' ファイル名を返り値に渡す
        strOutputOBSTrans += strDocFileName;
        return strOutputOBSTrans;
    }

    #endregion

    #endregion================================================================

    #endregion

    #region Private

    #region カウント レコード テーブル ObsExtendRefer
    /// <summary>
    /// カウント レコード テーブル ObsExtendRefer
    /// </summary>
    /// <param name="num">番号</param>
    /// <returns></returns>
    private int CountRecordTblObsExtendRefer(string num)
    {
        var obsExtendReferList = GetTblObsExtendReferList(num);
        int result = 0;
        if (obsExtendReferList != null) result = obsExtendReferList.Count;
        return result;
    }
    #endregion

    #region ObsextendRefer リストを取得
    /// <summary>
    /// obsExtendRefer リストを取得
    /// </summary>
    /// <param name="num">番号</param>
    /// <returns></returns>
    private List<ObsExtendReferDto>? GetTblObsExtendReferList(string num)
    {
        var obsExtendReferList = this.slaveDBContext?.ObsExtendRefers?
                                                     .FromSql($@"Select * from vw_obs_extend_refer;")
                                                     .ToList();
        List<ObsExtendReferDto>? result = null;
        if (obsExtendReferList != null) result = obsExtendReferList.Where(x => x.Num == num).ToList();
        return result;
    }
    #endregion

    #region 翻訳者を取得する
    /// <summary>
    /// 翻訳者を取得する
    /// </summary>
    /// <param name="pNo"></param>
    /// <returns></returns>
    private string L_getTranslator(string? pNo)
    {
        string result = string.Empty;
        WktTranslate? wktTranslate = new WktTranslate();
        wktTranslate = this.slaveDBContext?.WktTranslates?.Where(x => x.PlantName + "-" + x.Num == pNo && x.TransSituation == Constants.TRANS_SITUATION_CAPTURED).OrderBy(x => x.OfferNum).FirstOrDefault();
        if (wktTranslate != null && !string.IsNullOrEmpty(wktTranslate.Translator)) result = wktTranslate.Translator;
        return result;
    }
    #endregion

    #region DBと翻訳用Word文書の比較チェック
    /// <summary>
    ///'------------------------------------------------------------
    ///' DBと翻訳用Word文書の比較チェック
    ///'   bas_Word_Translate.cs
    ///'   V7.4r7 その他 Add
    ///'	p種類: pKinds	; p依頼番号 : offer_num	; pFile_name : p依頼番号 ; 処理内容: ProcessingContent
    ///'------------------------------------------------------------
    /// </summary>
    /// <param name="pKinds"></param>
    /// <param name="pOfferNum"></param>
    /// <param name="pProcessContent"></param>
    /// <returns></returns>
    private string CheckDBWordFile(string? pKinds, string pOfferNum, string pProcessContent)
    {
        string strCheckDBWordFile = string.Empty;
        string strNum = string.Empty;
        string strTranslang = string.Empty;

        int i = 0;
        string[] vntOriginal = new string[] { };
        bool[] blnFacts;
        bool[] blnConclusion;

        //'番号・翻訳言語・Wordファイル名の取得
        //' wkt_Translate テーブル
        WktTranslate? wktTranslate = new WktTranslate();
        wktTranslate = this.slaveDBContext?.WktTranslates?.Where(x => x.OfferNum == Convert.ToInt32(pOfferNum)).FirstOrDefault();
        if (wktTranslate != null && wktTranslate.Id > 0)
        {
            strNum = wktTranslate.PlantName + "-" + wktTranslate.Num;
            if (wktTranslate.TransLang != null) strTranslang = wktTranslate.TransLang;
        }
        else
        {
            strCheckDBWordFile = Constants.ENG_LANG;
            return strCheckDBWordFile;
        }

        switch (pKinds)
        {
            case Constants.KIND_WC:

                break;
            case Constants.KIND_OBS:
                //ObsExtendRefer
                //tbl_OBS_Fact
                ObsFact? obsFact = new ObsFact();
                obsFact = this.slaveDBContext?.ObsFacts?.Where(x => x.Num == strNum).FirstOrDefault();
                if (obsFact != null)
                {
                    if ((pProcessContent == Constants.PROCESS_CONTENT_OUTPUT && strTranslang == Constants.JAP_LANG) || (pProcessContent == Constants.PROCESS_DETAIL_CAPTURE && strTranslang == Constants.ENG_LANG))
                    {
                        if (obsFact?.FactTrans != null)
                        {
                            vntOriginal = new string[] { obsFact.FactTrans };
                        }
                    }
                    else
                    {
                        if (obsFact?.Fact != null)
                        {
                            vntOriginal = new string[] { obsFact.Fact };
                        }
                    }

                    if (vntOriginal.Length > 0)
                    {
                        blnFacts = new bool[vntOriginal.Length];
                        for (i = 0; i < vntOriginal.Length; i++)
                        {
                            if (string.IsNullOrEmpty(vntOriginal[i])) blnFacts[i] = true;
                        }
                    }
                }

                // tbl_OBS_Conclusion
                ObsConclusion? obsConclusion = new ObsConclusion();
                obsConclusion = this.slaveDBContext?.ObsConclusions?.Where(x => x.Num == strNum).FirstOrDefault();

                if (obsConclusion != null && obsConclusion.Num != string.Empty)
                {
                    if ((pProcessContent == Constants.PROCESS_CONTENT_OUTPUT && strTranslang == Constants.JAP_LANG) || (pProcessContent == Constants.PROCESS_DETAIL_CAPTURE && strTranslang == Constants.ENG_LANG))
                    {
                        if (obsConclusion?.ConclusionTrans != null)
                        {
                            vntOriginal = new string[] { obsConclusion.ConclusionTrans };
                        }
                    }
                    else
                    {
                        if (obsConclusion?.Conclusion != null)
                        {
                            vntOriginal = new string[] { obsConclusion.Conclusion };
                        }
                    }

                    if (vntOriginal.Length > 0)
                    {
                        blnConclusion = new bool[vntOriginal.Length];
                        for (i = 0; i < vntOriginal.Length; i++)
                        {
                            if (string.IsNullOrEmpty(vntOriginal[i])) blnConclusion[i] = true;
                        }
                    }
                }


                break;
            case Constants.KIND_GFA:

                break;
            case Constants.KIND_AFI:

                break;
            case Constants.KIND_STR:

                break;
            case Constants.KIND_PD:

                break;
            case Constants.KIND_BP:

                break;
            case Constants.KIND_SOI:

                break;
            default:
                break;
        }

        //'DBのデータ入力有無の確認
        return strCheckDBWordFile;
    }
    #endregion

    #endregion
}