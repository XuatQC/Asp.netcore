using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.Dto;
using Microsoft.EntityFrameworkCore;

namespace FNDSystem.Infrastructure.DataAccess.Repositories;
public class PeerReviewRepository : GenericRepository<PeerReview>, IPeerReviewRepository
{
    public PeerReviewRepository(DBMasterContext masterDBContext, DBSlaveContext slaveDBContext) : base(masterDBContext, slaveDBContext)
    {
    }

    #region PeerReview Master 取得
    /// <summary>
    /// PeerReview Master 取得
    /// </summary>
    /// <param name="peerReviewDto">検索条件</param>
    /// <returns>ピアレビュー（PR）一覧</returns>
    public IEnumerable<PeerReviewResponseDto>? GetPeerReviewMasterCheck(FindPeerReviewRequestDto peerReviewDto)
    {
        var result = this.slaveDBContext?.PeerReviewesponses?
                          .FromSql($@"Call Sp_Get_Peer_Review_Master({peerReviewDto.IsExistsField},{peerReviewDto.PlantName},{peerReviewDto.Initial});")
                          .ToList();
        return result;
    }
    #endregion
}