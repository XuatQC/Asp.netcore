using AutoMapper;
using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.Dto;
using FNDSystem.Core.Dto.OBS;
using FNDSystem.Core.Exceptions;
using FNDSystem.Core.Helpers;
using FNDSystem.Core.ServicesContracts;
using System.Linq.Expressions;

namespace FNDSystem.Core.Services;
public class ObsService : GeneticService, IObsService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public ObsService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    #region Obs

    #region 番号によりOBSを取得する
    /// <summary>
    /// 番号によりOBSを取得する
    /// </summary>
    /// <param name="num">番号</param>
    /// <returns>obs</returns>
    public Obs? GetObsByNum(string num)
    {
        Expression<Func<Obs, bool>> whereExp = obs => obs.Num == num;
        Obs? obsDb = unitOfWork.ObsRepository.Get(whereExp);
        return obsDb;
    }
    #endregion

    #region OBSリビジョン一覧を取得する
    /// <summary>
    /// OBSリビジョン一覧を取得する
    /// </summary>
    /// <param name="findObsDto">検索条件</param>
    /// <returns>OBS一覧</returns>
    public IEnumerable<NumberControl>? GetObsRevisionList(FindObsRequestDto findObsDto)
    {
        IEnumerable<NumberControl>? objItem = unitOfWork.NumberControlRepository
            .GetAll(x => x.NumNoRevisions == findObsDto.NumNoRevisions && x.Revisions < findObsDto.Revisions && !x.DeleteFlag)
            .OrderByDescending(x => x.Revisions);

        return objItem;
    }
    #endregion

    #region 複数条件でOBSを取得する
    /// <summary>
    /// 複数条件でOBSを取得する
    /// </summary>
    /// <param name="findObsDto">検索条件</param>
    /// <returns>OBS一覧</returns>
    public IEnumerable<Obs>? GetObsListByMultiCondition(FindObsRequestDto findObsDto)
    {
        IEnumerable<Obs>? objItem = unitOfWork.ObsRepository.GetObsListByMultiCodition(findObsDto);

        return objItem;
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
        IEnumerable<Obs>? objItem = unitOfWork.ObsRepository.GetObsListForReqTran2(obsDto);

        return objItem;
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
        var result = unitOfWork.ObsRepository.GetMaxNo(obsDto);

        return result;
    }
    #endregion

    #region GetNum
    /// <summary>
    /// OBSの番号とタイトルを取得する
    /// </summary>
    /// <param name="plantName">検索条件</param>
    /// <param name="fields">検索条件</param>
    /// <returns>OBSの番号とタイトル</returns>
    public IEnumerable<NumObsDto>? GetNum(string plantName, string fields)
    {
        IEnumerable<NumObsDto>? numObsDtos = unitOfWork.ObsRepository.GetNum(plantName, fields);
        return numObsDtos;
    }
    #endregion

    #region Create
    /// <summary>
    /// 新規作成
    /// </summary>
    /// <param name="obsDto">更新されたデータ</param>
    /// <returns>新しいob</returns>
    public Obs Create(ObsDto obsDto)
    {
        var obs = mapper.Map<Obs>(obsDto);

        unitOfWork.ObsRepository.Add(obs);
        unitOfWork.SaveChanges();

        return obs;
    }
    #endregion

    #region 番号によりOBSを更新する

    /// <summary>
    /// 番号によりOBSを更新する
    /// </summary>
    /// <param name="num">id</param>
    /// <param name="obsDto">更新されたデータ</param>
    /// <returns>新しいob</returns>
    public Obs? UpdateByNum(string? num, ObsExtend obsDto)
    {
        if (num == null) return null;
        Expression<Func<Obs, bool>> whereExp = obs => obs.Num == num && obs.DeleteFlag == obsDto.DeleteFlag;
        Obs? obsDb = unitOfWork.ObsRepository.Get(whereExp);
        if (obsDb == null) return null;
        obsDb.Title = obsDto.Title;
        obsDb.TitleTrans = obsDto.TitleTrans;
        obsDb.PartTrans = obsDto.PartTrans;
        obsDb.Scope = obsDto.Scope;
        obsDb.ScopeTrans = obsDto.ScopeTrans;
        obsDb.ScopeComment = obsDto.ScopeComment;
        obsDb.PartTransScope = obsDto.PartTransScope;
        obsDb.ObservationTarget = obsDto.ObservationTarget;
        obsDb.FinalVer = obsDto.FinalVer;
        obsDb.PackageExclude = obsDto.PackageExclude;
        obsDb.ValComp = obsDto.ValComp;
        obsDb.GeneralComment = obsDto.GeneralComment;
        obsDb.TransRange = obsDto.TransRange;
        obsDb.TransDeadline = obsDto.TransDeadline;
        obsDb.TransLang = obsDto.TransLang;
        obsDb.TransStateReq = obsDto.TransStateReq;
        obsDb.TransStateReqId = obsDto.TransStateReqId;
        obsDb.TransReqDate = obsDto.TransReqDate;
        obsDb.TranslatingState = obsDto.TranslatingState;
        obsDb.TranslatingStateId = obsDto.TranslatingStateId;
        obsDb.TranslatedState = obsDto.TranslatedState;
        obsDb.TranslatedStateId = obsDto.TranslatedStateId;
        obsDb.ApprovalStatePr = obsDto.ApprovalStatePr;
        obsDb.ApprovalStatePrId = obsDto.ApprovalStatePrId;
        obsDb.ApprovalStatePrRev = obsDto.ApprovalStatePrRev;
        obsDb.ApprovalStatePrState = obsDto.ApprovalStatePrState;
        obsDb.ApprovalState1 = obsDto.ApprovalState1;
        obsDb.ApprovalState1Id = obsDto.ApprovalState1Id;
        obsDb.ApprovalState1Rev = obsDto.ApprovalState1Rev;
        obsDb.ApprovalState2 = obsDto.ApprovalState2;
        obsDb.ApprovalState2Id = obsDto.ApprovalState2Id;
        obsDb.ApprovalState2Rev = obsDto.ApprovalState2Rev;
        obsDb.ApprovalState3 = obsDto.ApprovalState3;
        obsDb.ApprovalState3Id = obsDto.ApprovalState3Id;
        obsDb.ApprovalState3Rev = obsDto.ApprovalState3Rev;
        obsDb.ApprovalStateTl = obsDto.ApprovalStateTl;
        obsDb.ApprovalStateTlId = obsDto.ApprovalStateTlId;
        obsDb.ApprovalStateTlRev = obsDto.ApprovalStateTlRev;
        obsDb.ApprovalStateTlState = obsDto.ApprovalStateTlState;
        obsDb.Hold = obsDto.Hold;
        obsDb.DeleteFlag = obsDto.DeleteFlag;
        obsDb.Editor = obsDto.Editor;
        obsDb.LastUpdate = obsDto.LastUpdate;
        obsDb.LastUser = obsDto.LastUser;

        unitOfWork.ObsRepository.Update(obsDb);
        unitOfWork.SaveChanges();
        return obsDb;
    }
    #endregion


    #region OBSデータ削除
    /// <summary>
    /// OBSデータ削除
    /// </summary>
    /// <param name="deleteObsDto">データを削除する</param>
    /// <returns>true: 成功します。 false: 失敗します</returns>
    public bool Delete(DeleteObsDto deleteObsDto)
    {
        bool result = unitOfWork.ObsRepository.Delete(deleteObsDto);
        return result;
    }
    #endregion

    #region wktテーブルのデータを削除する
    /// <summary>
    /// wktテーブルのデータを削除する
    /// </summary>
    /// <returns>true: 成功します。 false: 失敗します</returns>
    public bool DeleteDataOfWktTables()
    {
        bool result = unitOfWork.ObsRepository.DeleteDataOfWktTables();
        return result;
    }
    #endregion

    #region AddNewObsAtScreenList
    /// <summary>
    /// OBS新規作成
    /// </summary>
    /// <param name="obsExtendDto">更新されたデータ</param>
    /// <returns>obs</returns>
    public Obs? AddNewObsAtScreenList(FindObsRequestDto obsExtendDto)
    {
        Obs? result = unitOfWork.ObsRepository.AddNewObsAtScreenList(obsExtendDto);
        return result;
    }
    #endregion

    #region OBSデータの保存処理
    /// <summary>
    /// OBSデータの保存処理
    /// </summary>
    /// <param name="userInitial">イニシャル</param>
    /// <param name="obsSaveDto">obs</param>
    /// <returns>番号</returns>
    public string ObsSaveProcess(string userInitial, ObsSaveDto obsSaveDto, string diskPath)
    {
        string num = string.Empty;
        ObsFact? obsFactSave = new();
        ObsConclusion? obsConclusionSave = new();
        List<ObsFact> obsFactSaves = new();
        List<ObsConclusion> obsConclusionSaves = new();
        using (var transaction = unitOfWork.BeginTransaction())
        {
            try
            {
                // RevUp保存押下時
                if (obsSaveDto.IsNewFlg)
                {
                    // 旧リビジョンの最新フラグをfalseにする
                    NumberControl? numberControl = unitOfWork.NumberControlRepository.Get(x => x.NumNoRevisions == obsSaveDto.Obs.NumNoRevisions && x.NewestFlag);
                    if (numberControl == null) throw new();
                    numberControl.NewestFlag = false;
                    var newBindingFactNum = new Dictionary<int, int>();
                    numberControl.LastUser = userInitial;
                    unitOfWork.NumberControlRepository.Update(numberControl);

                    obsSaveDto.Obs.TransDeadline = null;
                    obsSaveDto.Obs.TransLang = null;
                    obsSaveDto.Obs.TransReqDate = null;
                    obsSaveDto.Obs.TransStateReq = false;
                    obsSaveDto.Obs.TransStateReqId = null;
                    obsSaveDto.Obs.TranslatedState = false;
                    obsSaveDto.Obs.LastUser = userInitial;
                    obsSaveDto.Obs.LastUpdate = DateTime.Now;
                    obsSaveDto.Obs.ValComp = !obsSaveDto.Facts.Exists(x => !x.ValComp && x.PlusNeutralDelta != (int)Enums.PlusNeutralDelta.Neutral);

                    // 新リビジョンを元に番号を作成する
                    obsSaveDto.Obs.Num = StringUtils.GenerateNum(new NumberControl()
                    {
                        Num = string.Empty,
                        PlantName = obsSaveDto.Obs.PlantName,
                        Kinds = obsSaveDto.Obs.Kinds,
                        Fields = obsSaveDto.Obs.Fields,
                        SerialNum = obsSaveDto.Obs.SerialNum,
                        PartId = obsSaveDto.Obs.PartId,
                        Revisions = obsSaveDto.Obs.Revisions + 1,
                        Language = obsSaveDto.Obs.Language
                    });
                    num = obsSaveDto.Obs.Num;

                    // obs を挿入 & // obs_scope を挿入
                    var obs = mapper.Map<Obs>(obsSaveDto.Obs);
                    unitOfWork.ObsRepository.Add(obs);

                    // obs_fact を挿入
                    foreach (var (obsFactRequestDto, index) in obsSaveDto.Facts.WithIndex())
                    {
                        obsFactRequestDto.Num = obsSaveDto.Obs.Num;
                        obsFactRequestDto.DisplayOrder = index + 1;
                        obsFactRequestDto.LastUser = userInitial;
                        obsFactRequestDto.LastUpdate = DateTime.Now;
                        // 新規追加の場合はfactNumを作成する
                        if (obsFactRequestDto.FactNum < 0)
                        {
                            var oldFactNum = obsFactRequestDto.FactNum;
                            obsFactRequestDto.FactNum = obsSaveDto.Facts.Max(x => x.FactNum) + 1;
                            newBindingFactNum.Add(oldFactNum, obsFactRequestDto.FactNum);
                        }
                        obsFactSave = mapper.Map<ObsFact>(obsFactRequestDto);
                        obsFactSaves.Add(obsFactSave);

                        // obsAttach を挿入
                        int obsAttachMaxNumByFact = unitOfWork.ObsAttachRepository.MaxBy(x => x.SerialNum, x => x.FactNum == obsFactRequestDto.FactNum && x.NumNoRevisions == obsSaveDto.Obs.NumNoRevisions);
                        List<ObsAttach> obsAttachSave = new List<ObsAttach>();
                        foreach (ObsAttachRequestDto obsAttachRequestDto in obsFactRequestDto.ObsAttachs!)
                        {
                            //ユーザーが新しい写真を挿入した場合
                            if (obsAttachRequestDto.SerialNum == 0)
                            {
                                obsAttachMaxNumByFact++;
                                obsAttachRequestDto.SerialNum = obsAttachMaxNumByFact;
                                obsAttachRequestDto.FileName = obsSaveDto.Obs.NumNoRevisions + @"-" + $"{obsFactRequestDto.FactNum.ToString("00")}" + "-" + obsAttachRequestDto.SerialNum?.ToString("00") + "." + obsAttachRequestDto.Extension;
                                obsAttachRequestDto.LastUser = userInitial;
                                File.WriteAllBytes(diskPath + obsAttachRequestDto.FileName, obsAttachRequestDto.Src!);
                            }
                            obsAttachRequestDto.LastUpdate = DateTime.Now; 
                            obsAttachRequestDto.FactNum = obsFactRequestDto.FactNum;
                            var obsAttach = mapper.Map<ObsAttach>(obsAttachRequestDto);
                            obsAttachSave.Add(obsAttach);
                        }
                        unitOfWork.ObsAttachRepository.DeleteBy(x => x.FactNum == obsFactRequestDto.FactNum && x.NumNoRevisions == obsSaveDto.Obs.NumNoRevisions);
                        unitOfWork.ObsAttachRepository.AddRange(obsAttachSave);
                    }
                    unitOfWork.ObsFactRepository.AddRange(obsFactSaves);

                    // obs_conclusionを挿入
                    foreach (var (obsConclusionRequestDto, index) in obsSaveDto.Conclusions.WithIndex())
                    {
                        obsConclusionRequestDto.Num = obsSaveDto.Obs.Num;
                        obsConclusionRequestDto.DisplayOrder = index + 1;
                        if (!string.IsNullOrEmpty(obsConclusionRequestDto.ConnectFact))
                        {
                            List<string>  relatedFacts = obsConclusionRequestDto.ConnectFact.Split(",").ToList();
                            for (var rIndex = 0; rIndex < relatedFacts.Count; rIndex++)
                            {
                                if (int.Parse(relatedFacts[rIndex]) < 0)
                                {
                                    relatedFacts[rIndex] = newBindingFactNum[int.Parse(relatedFacts[rIndex])].ToString();
                                }
                            }
                            relatedFacts.RemoveAll(rf => !obsSaveDto.Facts.Exists(f => f.FactNum.ToString() == rf));
                            obsConclusionRequestDto.ConnectFact = string.Join(",", relatedFacts);
                        }
                        obsConclusionRequestDto.LastUser = userInitial;
                        obsConclusionRequestDto.LastUpdate = DateTime.Now;
                        // 新規追加の場合はconclusionNumを作成する
                        if (obsConclusionRequestDto.ConclusionNum == 0)
                        {
                            obsConclusionRequestDto.ConclusionNum = obsSaveDto.Conclusions.Max(x => x.ConclusionNum) + 1;
                        }
                        obsConclusionSave = mapper.Map<ObsConclusion>(obsConclusionRequestDto);
                        obsConclusionSaves.Add(obsConclusionSave);
                    }
                    unitOfWork.ObsConclusionRepository.AddRange(obsConclusionSaves);
                }
                else
                {
                    // obsを更新する
                    obsSaveDto.Obs.LastUser = userInitial;
                    obsSaveDto.Obs.LastUpdate = DateTime.Now;
                    obsSaveDto.Obs.ValComp = !obsSaveDto.Facts.Exists(x => !x.ValComp && x.PlusNeutralDelta != (int)Enums.PlusNeutralDelta.Neutral);
                    var updatedOBS = mapper.Map<Obs>(obsSaveDto.Obs);
                    unitOfWork.ObsRepository.Update(updatedOBS);
                    var newBindingFactNum = new Dictionary<int, int>();
                    // obs_factを更新する
                    // データベース内の最大factNumを取得する
                    int maxNum = unitOfWork.ObsFactRepository.MaxBy(x => x.FactNum, x => x.Num == obsSaveDto.Obs.Num);
                    // 更新前にDBの旧レコードを全て削除する
                    bool deleteResult = unitOfWork.ObsFactRepository.DeleteBy(x => x.Num == obsSaveDto.Obs.Num) > 0;
                    if (!deleteResult) throw new();
                    foreach (var (obsFactRequestDto, index) in obsSaveDto.Facts.WithIndex())
                    {
                        obsFactRequestDto.Num = obsSaveDto.Obs.Num;
                        obsFactRequestDto.DisplayOrder = index + 1;
                        obsFactRequestDto.LastUser = userInitial;
                        obsFactRequestDto.LastUpdate = DateTime.Now;
                        // 新規追加の場合はfactNumを作成する
                        if (obsFactRequestDto.FactNum < 0)
                        {
                            maxNum++;
                            newBindingFactNum.Add(obsFactRequestDto.FactNum, maxNum);
                            obsFactRequestDto.FactNum = maxNum;
                        }
                        obsFactSave = mapper.Map<ObsFact>(obsFactRequestDto);
                        obsFactSaves.Add(obsFactSave);

                        int obsAttachMaxNumByFact = unitOfWork.ObsAttachRepository.MaxBy(x => x.SerialNum, x => x.FactNum == obsFactRequestDto.FactNum && x.NumNoRevisions == obsSaveDto.Obs.NumNoRevisions);
                        List<ObsAttach> obsAttachSave = new List<ObsAttach>();
                        if (obsFactRequestDto.ObsAttachs == null) continue;
                        foreach(ObsAttachRequestDto obsAttachRequestDto in obsFactRequestDto.ObsAttachs)
                        {
                            if(obsAttachRequestDto.SerialNum == 0)
                            {
                                obsAttachMaxNumByFact++;
                                obsAttachRequestDto.SerialNum = obsAttachMaxNumByFact;
                                obsAttachRequestDto.FileName = obsSaveDto.Obs.NumNoRevisions + @"-"+ $"{obsFactRequestDto.FactNum.ToString("00")}" + "-" + obsAttachRequestDto.SerialNum?.ToString("00") + "." + obsAttachRequestDto.Extension;
                                obsAttachRequestDto.LastUser = userInitial;
                                File.WriteAllBytes(diskPath + obsAttachRequestDto.FileName, obsAttachRequestDto.Src!);
                            }
                            obsAttachRequestDto.LastUpdate = DateTime.Now;
                                obsAttachRequestDto.FactNum = obsFactRequestDto.FactNum;
                            var obsAttach = mapper.Map<ObsAttach>(obsAttachRequestDto);
                            obsAttachSave.Add(obsAttach);
                        }
                        unitOfWork.ObsAttachRepository.DeleteBy(x => x.FactNum == obsFactRequestDto.FactNum && x.NumNoRevisions == obsSaveDto.Obs.NumNoRevisions);
                        unitOfWork.ObsAttachRepository.AddRange(obsAttachSave);
                    }
                    unitOfWork.ObsFactRepository.AddRange(obsFactSaves);

                    // obs_conclusionを更新する
                    // DBの最大conclusionNumを取得する
                    maxNum = unitOfWork.ObsConclusionRepository.MaxBy(x => x.ConclusionNum, x => x.Num == obsSaveDto.Obs.Num);
                    // 更新前にDBの旧レコードを全て削除する
                    deleteResult = unitOfWork.ObsConclusionRepository.DeleteBy(x => x.Num == obsSaveDto.Obs.Num) > 0;
                    if (!deleteResult) throw new();
                    foreach (var (obsConclusionRequestDto, index) in obsSaveDto.Conclusions.WithIndex())
                    {
                        obsConclusionRequestDto.Num = obsSaveDto.Obs.Num;
                        obsConclusionRequestDto.DisplayOrder = index + 1;
                        if (!string.IsNullOrEmpty(obsConclusionRequestDto.ConnectFact))
                        {
                            List<string> relatedFacts = obsConclusionRequestDto.ConnectFact.Split(",").ToList();
                            for(var rIndex = 0; rIndex < relatedFacts.Count; rIndex++)
                            {
                                if (int.Parse(relatedFacts[rIndex]) < 0)
                                {
                                    relatedFacts[rIndex] = newBindingFactNum[int.Parse(relatedFacts[rIndex])].ToString();
                                }
                            }
                            relatedFacts.RemoveAll(rf => !obsSaveDto.Facts.Exists(f => f.FactNum.ToString() == rf));
                            obsConclusionRequestDto.ConnectFact = string.Join(",", relatedFacts);
                        }
                        obsConclusionRequestDto.LastUser = userInitial;
                        obsConclusionRequestDto.LastUpdate = DateTime.Now;
                        // 新規追加の場合はconclusionNumを作成する
                        if (obsConclusionRequestDto.ConclusionNum == 0)
                        {
                            maxNum++;
                            obsConclusionRequestDto.ConclusionNum = maxNum;
                        }
                        obsConclusionSave = mapper.Map<ObsConclusion>(obsConclusionRequestDto);
                        obsConclusionSaves.Add(obsConclusionSave);
                    }
                    unitOfWork.ObsConclusionRepository.AddRange(obsConclusionSaves);
                    num = obsSaveDto.Obs.Num;
                }
                // DBに更新する
                unitOfWork.SaveChanges();
                transaction.Commit();
            }
            catch(Exception ex)
            {
                // エラーが発生した時にデータを復旧する
                transaction.Rollback();
                throw;
            }
        }

        return num;
    }
    #endregion

    #region テーブルObsWorkの変換
    /// <summary>
    /// テーブルObsWorkの変換
    /// </summary>
    /// <param name="num">番号</param>
    /// <param name="transSource">ソースを翻訳する</param>
    /// <returns>true: 成功します。 false: 失敗します</returns>
    public bool ObsWorkTableConversion(string num, string transSource)
    {
        List<ObsFact> obsFactDbList = new List<ObsFact>();
        List<ObsFactPast>? obsFactPastDbList = new List<ObsFactPast>();
        List<WktObsFact> wktObsFactInsList = new List<WktObsFact>();
        List<ObsConclusionPast>? obsConclusionPastDbList = new List<ObsConclusionPast>();
        List<ObsConclusion> obsConclusionDbList = new List<ObsConclusion>();
        List<WktObsConclusion> obsConclusionInsList = new List<WktObsConclusion>();

        using (var transaction = unitOfWork.BeginTransaction())
        {
            try
            {
                // 'OBC観察事実からwktOBC観察事実を作成
                WktObsFact wktObsFact = new WktObsFact() { Id = 0 };
                unitOfWork.WktObsFactRepository.DeleteAll(wktObsFact);

                int intSerialNum = 1;
                if (transSource == Constants.TRANS_SOURCE_PAST)
                {
                    Expression<Func<ObsFactPast, bool>> whereExp = obs => obs.Num == num;
                    obsFactPastDbList = unitOfWork.ObsFactPastRepository.GetAll(whereExp).ToList();

                    foreach (ObsFactPast item in obsFactPastDbList)
                    {
                        wktObsFactInsList.Add(new WktObsFact()
                        {
                            Num = num,
                            SerialNum = intSerialNum,
                            Fact = item.Fact,
                            FactTrans = item.FactTrans,
                            Comment = item.Comment,
                            PlusNeutralDelta = item.PlusNeutralDelta,
                        });
                        intSerialNum++;
                    }
                }
                else
                {
                    Expression<Func<ObsFact, bool>> whereExp = obs => obs.Num == num;
                    obsFactDbList = unitOfWork.ObsFactRepository.GetAll(whereExp).ToList();
                    foreach (ObsFact item in obsFactDbList)
                    {
                        wktObsFactInsList.Add(new WktObsFact()
                        {
                            Num = num,
                            SerialNum = intSerialNum,
                            Fact = item.Fact,
                            FactTrans = item.FactTrans,
                            Comment = item.Comment,
                            PlusNeutralDelta = item.PlusNeutralDelta
                        });
                        intSerialNum++;
                    }
                }

                if (wktObsFactInsList.Count > 0)
                {
                    foreach (WktObsFact item in wktObsFactInsList)
                    {
                        unitOfWork.WktObsFactRepository.Add(item);
                    }
                }

                //'OBS結論からwktOBS結論を作成
                WktObsConclusion wktObsConclusion = new WktObsConclusion() { Id = 0 };
                unitOfWork.WktObsConclusionRepository.DeleteAll(wktObsConclusion);

                intSerialNum = 1;
                if (transSource == Constants.TRANS_SOURCE_PAST)
                {
                    Expression<Func<ObsConclusionPast, bool>> whereExp = obs => obs.Num == num;
                    obsConclusionPastDbList = unitOfWork.ObsConclusionPastRepository.GetAll(whereExp).ToList();

                    foreach (ObsConclusionPast item in obsConclusionPastDbList)
                    {
                        obsConclusionInsList.Add(new WktObsConclusion()
                        {
                            Num = num,
                            SerialNum = intSerialNum,
                            Conclusion = item.Conclusion,
                            ConclusionTrans = item.ConclusionTrans,
                            Comment = item.Comment,
                            ConnectFact = item.ConnectFact
                        });
                        intSerialNum++;
                    }
                }
                else
                {
                    Expression<Func<ObsConclusion, bool>> whereExp = obs => obs.Num == num;
                    obsConclusionDbList = unitOfWork.ObsConclusionRepository.GetAll(whereExp).ToList();
                    foreach (ObsConclusion item in obsConclusionDbList)
                    {
                        obsConclusionInsList.Add(new WktObsConclusion()
                        {
                            Num = num,
                            SerialNum = intSerialNum,
                            Conclusion = item.Conclusion,
                            ConclusionTrans = item.ConclusionTrans,
                            Comment = item.Comment,
                            ConnectFact = item.ConnectFact
                        });
                        intSerialNum++;
                    }
                }

                if (obsConclusionInsList.Count > 0)
                {
                    foreach (WktObsConclusion item in obsConclusionInsList)
                    {
                        unitOfWork.WktObsConclusionRepository.Add(item);
                    }
                }

                unitOfWork.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }
    }
    #endregion

    #endregion

    #region ObsExtend

    #region ObsExtend一覧取得
    /// <summary>
    /// ObsExtend一覧取得
    /// </summary>
    /// <param name="obsExtendDto">検索条件</param>
    /// <returns>OBS一覧</returns>
    public IEnumerable<DisObsReponseDto>? GetObsExtendList(FindObsRequestDto obsExtendDto)
    {
        var result = unitOfWork.ObsRepository.GetObsExtendList(obsExtendDto);

        return result;
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
        var result = unitOfWork.ObsRepository.GetObsExtendListWhenClearClick(obsExtendDto);

        return result;
    }
    #endregion

    #region ObsExtendの件数を取得する
    /// <summary>
    /// ObsExtendの件数を取得する
    /// </summary>
    /// <param name="obsExtendDto">検索条件</param>
    /// <returns>オブセクテンドの記録</returns>
    public int GetCountObsExtendList(FindObsRequestDto obsExtendDto)
    {
        int result = 0;
        var list = unitOfWork.ObsRepository.GetObsExtends(obsExtendDto.PlantName + "-" + obsExtendDto.LNum);
        if (list != null && list?.Count() > 0) result = Convert.ToInt32(list?.Count());

        return result;

    }
    #endregion

    #region ObsExtendテーブルのデータを取得する
    /// <summary>
    /// ObsExtendテーブルのデータを取得する
    /// </summary>
    /// <param name="obsExtendDto">検索条件</param>
    /// <returns>obsextend一覧</returns>
    public IEnumerable<VwObsControl>? GetOnlyObsExtendList(FindObsRequestDto obsExtendDto)
    {
        Expression<Func<VwObsControl, bool>> whereExp = obs => obs.Num == obsExtendDto.Num && obs.PartTrans == true && obs.PlantName == obsExtendDto.PlantName;

        var result = unitOfWork.VwObsControlRepository.GetAll(whereExp);

        return result;
    }
    #endregion

    #region ObsExtendテーブルのデータを取得する
    /// <summary>
    /// ObsExtendテーブルのデータを取得する
    /// </summary>
    /// <param name="num">番号</param>
    /// <returns>ObsExtend</returns>
    public ObsExtend? GetOnlyObsExtend(string num)
    {
        var result = unitOfWork.ObsRepository.GetObsExtends(num)?.FirstOrDefault();

        return result;
    }
    #endregion

    #endregion

    #region ObsExtendReferの番号を取得する
    /// <summary>
    /// ObsExtendReferの番号を取得する
    /// </summary>
    /// <param name="findObsExtenReferDto">検索条件</param>
    /// <returns>番号</returns>
    public string GetObsExtendReferNum(FindObsRequestDto findObsExtenReferDto)
    {
        var num = unitOfWork.ObsRepository.GetReqNumOfObsExtendRefer(findObsExtenReferDto);
        return num;
    }

    /// <summary>
    /// 番号によりObsExtendRefer一覧を取得する
    /// </summary>
    /// <param name="findObsExtendReferDto">検索条件</param>
    /// <returns>OBSExtend一覧</returns>
    public IEnumerable<ObsExtendReferDto>? GetObsExtendReferListByNum(FindObsExtendReferRequestDto findObsExtendReferDto)
    {
        var obsExtenReferList = unitOfWork.ObsRepository.GetObsExtendReferListByNum(findObsExtendReferDto);

        return obsExtenReferList;
    }

    #endregion

}