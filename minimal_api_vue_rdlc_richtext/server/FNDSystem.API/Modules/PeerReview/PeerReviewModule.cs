using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;
using FNDSystem.Core.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using Modules.Shared;

namespace FNDSystem
{
    public class PeerReviewModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            return builder;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/peer-review/list/get-by-plant-name/", GetListByPlantName)
                      .Produces<IEnumerable<PeerReview>>(200);

            endpoints.MapGet("/peer-review/get-by-hold/", GetPeerReviewListByHold)
                     .Produces<IEnumerable<PeerReview>>(200);

            endpoints.MapGet("/peer-review/{plantName}", GetPeerReviewByPlantName)
                     .Produces<PeerReview>(200);

            endpoints.MapGet("/peer-review/get-by-plant-name-hold/", GetListByPlantNameAndHold)
                     .Produces<IEnumerable<PeerReview>>(200);

            endpoints.MapGet("/peer-review/master-check/", GetPeerReviewMasterCheck)
                     .Produces<IEnumerable<PeerReviewResponseDto>>(200);
            return endpoints;
        }

        #region 当該PRの提供先区分をピアレビューテーブルから取得する。
        /// <summary>
        /// 9.1.1 当該PRの提供先区分をピアレビューテーブルから取得する。
        /// </summary>
        /// <param name="findPeerReviewDto">画面.プラント名</param>
        /// <param name="peerReviewService">奉仕</param>
        /// <returns>ピアレビューテーブル一覧</returns>
        private R<IEnumerable<PeerReview>> GetListByPlantName([AsParameters] FindPeerReviewRequestDto findPeerReviewDto, [FromServices] IPeerReviewService peerReviewService)
        {
            var response = peerReviewService.GetByPlantName(findPeerReviewDto);
            return new R<IEnumerable<PeerReview>>()
            {
                Payload = response
            };
        }
        #endregion

        #region プラント名の選択肢
        /// <summary>
        /// プラント名の選択肢
        /// </summary>
        /// <param name="peerReviewService">奉仕</param>
        /// <returns>ピアレビューテーブル一覧</returns>
        private R<IEnumerable<PeerReview>> GetPeerReviewListByHold([FromServices] IPeerReviewService peerReviewService)
        {
            var response = peerReviewService.GetListByHold();
            return new R<IEnumerable<PeerReview>>()
            {
                Payload = response
            };
        }
        #endregion

        #region ピアレビューテーブルからデータを取得します。
        /// <summary>
        /// ピアレビューテーブルからデータを取得します。
        /// </summary>
        /// <param name="plantName">プラント名</param>
        /// <param name="peerReviewService">奉仕</param>
        /// <returns>ピアレビューテーブル</returns>
        public R<PeerReview?> GetPeerReviewByPlantName(string plantName, [FromServices] IPeerReviewService peerReviewService)
        {
            var response = peerReviewService.GetPeerReviewByPlantName(plantName);
            return new R<PeerReview?>()
            {
                Payload = response
            };
        }

        #endregion

        #region プラント名と開催中で一覧を取得する
        /// <summary>
        /// プラント名と開催中で一覧を取得する
        /// </summary>
        /// <param name="peerReviewService">奉仕</param>
        /// <returns>ピアレビュー（PR）一覧</returns>
        public R<IEnumerable<PeerReview>> GetListByPlantNameAndHold([FromServices] IPeerReviewService peerReviewService)
        {
            var response = peerReviewService.GetListByPlantNameAndHold();
            return new R<IEnumerable<PeerReview>>()
            {
                Payload = response
            };
        }
        #endregion

        #region PeerReview Master 取得
        /// <summary>
        /// PeerReview Master 取得
        /// </summary>
        /// <param name="findPeerReviewDto">検索条件</param>
        /// <param name="peerReviewService">奉仕</param>
        /// <returns>ピアレビュー（PR）一覧</returns>
        public R<IEnumerable<PeerReviewResponseDto>> GetPeerReviewMasterCheck([AsParameters] FindPeerReviewRequestDto findPeerReviewDto, [FromServices] IPeerReviewService peerReviewService)
        {
            var response = peerReviewService.GetPeerReviewMasterCheck(findPeerReviewDto);
            return new R<IEnumerable<PeerReviewResponseDto>>()
            {
                Payload = response
            };
        }
        #endregion
    }
}