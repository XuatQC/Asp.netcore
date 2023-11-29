using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.Dto;
using FNDSystem.Core.Extensions;
using FNDSystem.Core.ServicesContracts;
using System.Linq.Expressions;

namespace FNDSystem.Core.Services;
public class ReviewMemberService : GeneticService, IReviewMemberService
{
    private readonly IUnitOfWork unitOfWork;

    public ReviewMemberService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    #region ピアレビューメンバマスタの一覧取得
    /// <summary>
    /// ピアレビューメンバマスタの一覧取得
    /// </summary>
    /// <param name="findDto">検索条件</param>
    /// <returns>ピアレビューメンバマスタ 一覧</returns>
    public IEnumerable<ReviewMemberResponseDto>? GetReviewMemberList(FindReviewMemberRequestDto findDto)
    {
        IEnumerable<ReviewMemberResponseDto>? resultList = unitOfWork.ReviewMemberRepository.GetReviewMemberList(findDto);

        return resultList;
    }
    #endregion

    #region ピアレビューメンバマスタの取得
    /// <summary>
    /// ピアレビューメンバマスタの取得
    /// </summary>
    /// <param name="findDto">検索条件</param>
    /// <returns>ピアレビューメンバマスタ</returns>
    public ReviewMember? GetReviewMember(FindReviewMemberRequestDto findDto)
    {
        Expression<Func<ReviewMember, bool>> whereExp = item => item.PlantName == findDto.PlantName;
        if (!string.IsNullOrEmpty(findDto.Initial))
        {
            Expression<Func<ReviewMember, bool>> byInitial = item => item.Initial == findDto.Initial;
            whereExp = whereExp.AndEx<ReviewMember>(byInitial);
        }
        var result = unitOfWork.ReviewMemberRepository.Get(whereExp);

        return result;
    }
    #endregion

    #region GetReviewMemberEditorList
    /// <summary>
    /// plantNameでReviewMemberの一覧を取得する
    /// </summary>
    /// <param name="plantname"></param>
    /// <returns></returns>
    public IEnumerable<ReviewMemberEditorResponseDto>? GetReviewMemberEditorList(string plantName)
    {
        return unitOfWork.ReviewMemberRepository.GetReviewMemberEditor(plantName);

    }

    #endregion
}