using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;

namespace FNDSystem.Core.Domain.RepositoryContracts;
public interface IPeerReviewRepository : IGenericRepository<PeerReview>
{
    IEnumerable<PeerReviewResponseDto>? GetPeerReviewMasterCheck(FindPeerReviewRequestDto peerReviewDto);
}