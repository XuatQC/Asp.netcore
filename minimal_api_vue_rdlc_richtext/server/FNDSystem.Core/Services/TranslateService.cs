using AutoMapper;
using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.Dto;
using FNDSystem.Core.Extensions;
using FNDSystem.Core.ServicesContracts;
using System.Linq.Expressions;

namespace FNDSystem.Core.Services;
public class TranslateService : GeneticService, ITranslateService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public TranslateService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    /// <summary>
    /// 翻訳情報取得
    /// </summary>
    /// <param name="findTranslateDto">検索条件</param>
    /// <returns>WORDファイル格納情報ファイル</returns>
    public VwTranslateControl? GetTranslate(FindTranslateRequestDto findTranslateDto)
    {
        Expression<Func<VwTranslateControl, bool>> whereExpression = item => item.OfferNum == findTranslateDto.OfferNum;

        if (!string.IsNullOrEmpty(findTranslateDto.PlantName))
        {
            Expression<Func<VwTranslateControl, bool>> byPlantName = item => item.PlantName == findTranslateDto.PlantName;
            whereExpression = whereExpression.AndEx<VwTranslateControl>(byPlantName);
        }

        var result = unitOfWork.VwTranslateControlRepository.Get(whereExpression);

        return result;
    }

    /// <summary>
    /// 条件で一覧を取得する
    /// </summary>
    /// <param name="findTranslateDto">検索条件</param>
    /// <returns>WORDファイル格納情報ファイル一覧</returns>
    public IEnumerable<VwTranslateControl>? GetMany(FindTranslateRequestDto findTranslateDto)
    {
        Expression<Func<VwTranslateControl, bool>> whereExpression = item => item.OfferNum == findTranslateDto.OfferNum;

        if (!string.IsNullOrEmpty(findTranslateDto.PlantName))
        {
            Expression<Func<VwTranslateControl, bool>> byPlantName = item => item.PlantName == findTranslateDto.PlantName;
            whereExpression = whereExpression.AndEx<VwTranslateControl>(byPlantName);
        }

        var result = unitOfWork.VwTranslateControlRepository.GetAll(whereExpression);

        return result;
    }

    #region OfferNum一覧を取得する
    /// <summary>
    /// OfferNum一覧を取得する
    /// </summary>
    /// <param name="findTranslateDto">検索条件</param>
    /// <returns>OfferNum一覧</returns>
    public List<OfferNumResponseDto>? GetOfferNumList(FindTranslateRequestDto findTranslateDto)
    {
        var result = unitOfWork.TranslateRepository.GetOfferNumList(findTranslateDto);

        return result;
    }
    #endregion

    /// <summary>
    /// 翻訳データ作成
    /// </summary>
    /// <param name="findTranslateDto">WORDファイル格納情報ファイル</param>
    /// <param name="pTableName">テーブル名</param>
    /// <param name="gPlantName">プラント名</param>
    /// <returns>true: 成功します。 false: 失敗します</returns>
    public bool CreateTransData(FindTranslateRequestDto findTranslateDto, string pTableName, string gPlantName)
    {
        var result = unitOfWork.TranslateRepository.CreateTranslationData(findTranslateDto, pTableName, gPlantName);

        return result;
    }

    /// <summary>
    /// 翻訳が完了したか確認
    /// </summary>
    /// <param name="num">番号</param>
    /// <returns>true: 成功します。 false: 失敗します</returns>
    public bool IsTransCompConfirm(string num)
    {
        bool result = false;
        Expression<Func<Translate, bool>> whereExpression = item => item.Num == num &&
                                                                    item.TransSituation == Constants.TRANS_SITUATION_PRINTED &&
                                                                    item.TransArrival == "●" &&
                                                                    (item.CancelStatus == "" || item.CancelStatus == Constants.CANCEL_STATUS_CONTINUE_TRANS);

        var transList = unitOfWork.TranslateRepository.GetAll(whereExpression);
        if (transList != null && transList?.Count() > 0)
        {
            result = true;
        }
        return result;
    }

    /// <summary>
    /// 翻訳データ取得
    /// </summary>
    /// <param name="plantName">プラント名</param>
    /// <returns>true: 成功します。 false: 失敗します</returns>
    public bool LoadTranslateData(string plantName)
    {
        // wkt-Translateからすべてのレコードを削除します
        unitOfWork.WktTranslateRepository.DeleteAll();
        Expression<Func<VwTranslateControl, bool>> whereExpression = item => item.PlantName == plantName;
        List<VwTranslateControl> result = unitOfWork.VwTranslateControlRepository.GetAll(whereExpression, 0, null).ToList();

        result.ForEach((item) =>
        {
            var selectCancelStatus = "";
            if (item.EnglishEdition == Constants.ENG_LANG)
            {
                switch (item.CancelStatus)
                {
                    case Constants.CANCEL_STATUS_REQUESTING_CANCEL:
                        {
                            selectCancelStatus = "Cancel Requested";
                            break;
                        }
                    case Constants.CANCEL_STATUS_CANCELED:
                        {
                            selectCancelStatus = "Canceled";
                            break;
                        }
                    case Constants.CANCEL_STATUS_CONTINUE_TRANS:
                        {
                            selectCancelStatus = "Trans. Continued";
                            break;
                        }
                    default:
                        {
                            selectCancelStatus = item.CancelStatus;
                            break;
                        }
                }
            }
            var instance = mapper.Map<WktTranslate>(item);
            instance.SelectionCancelStatus = selectCancelStatus;
            unitOfWork.WktTranslateRepository.Add(instance);
            unitOfWork.SaveChanges();
        });
        return true;
    }
}