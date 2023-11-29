using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.Dto;
using FNDSystem.Core.Extensions;
using FNDSystem.Core.ServicesContracts;
using System.Linq.Expressions;

namespace FNDSystem.Core.Services;
public class WktTranslateService : GeneticService, IWktTranslateService
{
    private readonly IUnitOfWork unitOfWork;
    public WktTranslateService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    /// <summary>
    /// 翻訳一時表の情報を取得する
    /// </summary>
    /// <param name="findWktTranslateDto">条件検索</param>
    /// <returns>翻訳一時表の情報</returns>
    public WktTranslate? GetWktTranslate(FindWktTranslateRequestDto findWktTranslateDto)
    {
        Expression<Func<WktTranslate, bool>> whereExpression = item => item.OfferNum == findWktTranslateDto.OfferNum;

        if (!string.IsNullOrEmpty(findWktTranslateDto.PlantName))
        {
            Expression<Func<WktTranslate, bool>> byPlantName = item => item.PlantName == findWktTranslateDto.PlantName;
            whereExpression = whereExpression.AndEx<WktTranslate>(byPlantName);
        }

        var result = unitOfWork.WktTranslateRepository.Get(whereExpression);

        return result;
    }

    /// <summary>
    /// 翻訳一時表の情報一覧を取得する
    /// </summary>
    /// <param name="findWktTranslateDto">条件検索</param>
    /// <returns>翻訳一時表の情報一覧</returns>
    public IEnumerable<WktTranslate>? GetMany(FindWktTranslateRequestDto findWktTranslateDto)
    {
        Expression<Func<WktTranslate, bool>> whereExpression = item => item.OfferNum == findWktTranslateDto.OfferNum;

        if (!string.IsNullOrEmpty(findWktTranslateDto.PlantName))
        {
            Expression<Func<WktTranslate, bool>> byPlantName = item => item.PlantName == findWktTranslateDto.PlantName;
            whereExpression = whereExpression.AndEx<WktTranslate>(byPlantName);
        }

        var result = unitOfWork.WktTranslateRepository.GetAll(whereExpression);

        return result;
    }

    /// <summary>
    /// データ削除
    /// </summary>
    public void DeleteAll()
    {
        unitOfWork.WktTranslateRepository.DeleteAll();
    }
}