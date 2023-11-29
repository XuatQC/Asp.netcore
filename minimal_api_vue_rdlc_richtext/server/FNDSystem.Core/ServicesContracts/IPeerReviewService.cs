using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;

namespace FNDSystem.Core.ServicesContracts;

public interface IPeerReviewService
{
    IEnumerable<PeerReview>? GetByPlantName(FindPeerReviewRequestDto findPeerReviewDto);

    IEnumerable<PeerReview>? GetListByHold();

    PeerReview? GetPeerReviewByPlantName(string plantName);

    IEnumerable<PeerReview>? GetListByPlantNameAndHold();

    IEnumerable<PeerReviewResponseDto>? GetPeerReviewMasterCheck(FindPeerReviewRequestDto peerReviewDto);
}