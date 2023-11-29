using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;
using FNDSystem.Core.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using Modules.Shared;

namespace FNDSystem
{
    public class ReviewMemberModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            return builder;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/review-member/list", GetReviewMemberList)
                     .Produces<IEnumerable<ReviewMemberResponseDto>>(200);

            endpoints.MapGet("/review-member/", GetReviewMember)
                     .Produces<ReviewMember>(200);

            endpoints.MapGet("/review-member/{plantName}", GetEditorListByReviewMember)
                     .Produces<IEnumerable<ReviewMemberEditorResponseDto>>(200);

            return endpoints;
        }

        #region ピアレビューメンバマスタの一覧取得
        /// <summary>
        /// ピアレビューメンバマスタの一覧取得
        /// </summary>
        /// <param name="findDto">検索条件</param>
        /// <param name="reviewMemberService">奉仕</param>
        /// <returns>ピアレビューメンバマスタ 一覧</returns>
        private R<IEnumerable<ReviewMemberResponseDto>> GetReviewMemberList([AsParameters] FindReviewMemberRequestDto findDto, [FromServices] IReviewMemberService reviewMemberService)
        {
            var response = reviewMemberService.GetReviewMemberList(findDto);
            return new R<IEnumerable<ReviewMemberResponseDto>>()
            {
                Payload = response
            };
        }
        #endregion

        #region ピアレビューメンバマスタの取得
        /// <summary>
        /// ピアレビューメンバマスタの取得
        /// </summary>
        /// <param name="findDto">検索条件</param>
        /// <param name="reviewMemberService">奉仕</param>
        /// <returns>ピアレビューメンバマスタ</returns>
        private R<ReviewMember> GetReviewMember([AsParameters] FindReviewMemberRequestDto findDto, [FromServices] IReviewMemberService reviewMemberService)
        {
            var response = reviewMemberService.GetReviewMember(findDto);
            return new R<ReviewMember>()
            {
                Payload = response
            };
        }
        #endregion

        #region plantNameでReviewMemberEditorの一覧を取得する
        /// <summary>
        /// plantNameでReviewMemberEditorの一覧を取得する
        /// </summary>
        /// <param name="plantName"></param>
        /// <param name="reviewMemberService"></param>
        /// <returns></returns>
        public R<IEnumerable<ReviewMemberEditorResponseDto>> GetEditorListByReviewMember(string plantName, [FromServices] IReviewMemberService reviewMemberService)
        {
            return new R<IEnumerable<ReviewMemberEditorResponseDto>>()
            {
                Payload = reviewMemberService.GetReviewMemberEditorList(plantName)
            };
        }
        #endregion
    }
}