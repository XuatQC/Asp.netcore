using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.Dto;
using FNDSystem.Core.ServicesContracts;
using System.Linq.Expressions;

namespace FNDSystem.Core.Services;
public class PeerReviewService : GeneticService, IPeerReviewService
{
    private readonly IUnitOfWork unitOfWork;

    public PeerReviewService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    /// <summary>
    /// 9.1.1 当該PRの提供先区分をピアレビュー（PR）から取得する。
    /// </summary>
    /// <param name="findPeerReviewDto">画面.プラント名</param>
    /// <returns>List ピアレビューテーブル</returns>
    public IEnumerable<PeerReview>? GetByPlantName(FindPeerReviewRequestDto findPeerReviewDto)
    {
        Expression<Func<PeerReview, bool>>? whereExpression = pr => pr.PlantName == findPeerReviewDto.PlantName;
        var peerReview = unitOfWork.PeerReviewRepository.GetAll(whereExpression);
        return peerReview;
    }

    /// <summary>
    ///  プラント名の選択肢
    /// </summary>
    /// <returns>ピアレビューテーブル一覧</returns>
    public IEnumerable<PeerReview>? GetListByHold()
    {
        Expression<Func<PeerReview, bool>> whereExpression = pr => pr.Hold;
        var PeerReview = unitOfWork.PeerReviewRepository.GetAll(whereExpression, 0, null);
        return PeerReview;
    }

    /// <summary>
    /// ピアレビューテーブルからデータを取得します。
    /// </summary>
    /// <param name="plantName">プラント名</param>
    /// <returns>ピアレビューテーブル</returns>
    public PeerReview? GetPeerReviewByPlantName(string plantName)
    {
        Expression<Func<PeerReview, bool>> whereExpression = item => item.PlantName == plantName;

        var peerReviews = unitOfWork.PeerReviewRepository.Get(whereExpression);
        return peerReviews;
    }

    /// <summary>
    /// プラント名と開催中で一覧を取得する
    /// </summary>
    /// <returns>ピアレビュー（PR）一覧</returns>
    public IEnumerable<PeerReview>? GetListByPlantNameAndHold()
    {
        Expression<Func<PeerReview, bool>> whereExpression = pr => !string.IsNullOrEmpty(pr.PlantName) && pr.Hold;

        var peerReview = unitOfWork.PeerReviewRepository.GetAll(whereExpression);
        return peerReview;
    }

    /// <summary>
    /// PeerReview Master 取得
    /// </summary>
    /// <param name="peerReviewDto">検索条件</param>
    /// <returns>ピアレビュー（PR）一覧</returns>
    public IEnumerable<PeerReviewResponseDto>? GetPeerReviewMasterCheck(FindPeerReviewRequestDto peerReviewDto)
    {
        var peerReviews = unitOfWork.PeerReviewRepository.GetPeerReviewMasterCheck(peerReviewDto);
        return peerReviews;
    }
}