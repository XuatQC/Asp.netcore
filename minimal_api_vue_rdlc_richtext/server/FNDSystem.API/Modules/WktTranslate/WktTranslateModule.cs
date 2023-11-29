using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;
using FNDSystem.Core.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using Modules.Shared;

namespace FNDSystem
{
    public class WktTranslateModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/wkt-translate/", GetWktTranslate)
                     .Produces<WktTranslate>(200);

            endpoints.MapGet("/wkt-translate/list", FinManyHandler)
                     .Produces<IEnumerable<WktTranslate>>(200);

            return endpoints;
        }

        #region 翻訳一時表の情報を取得する
        /// <summary>
        /// 翻訳一時表の情報を取得する
        /// </summary>
        /// <param name="findWktTranslateDto">条件検索</param>
        /// <param name="wktTranslateService">奉仕</param>
        /// <returns>翻訳一時表の情報</returns>
        private R<WktTranslate> GetWktTranslate([AsParameters] FindWktTranslateRequestDto findWktTranslateDto, [FromServices] IWktTranslateService wktTranslateService)
        {
            var response = wktTranslateService.GetWktTranslate(findWktTranslateDto);
            return new R<WktTranslate>()
            {
                Payload = response
            };
        }

        #endregion

        #region 翻訳一時表の情報一覧を取得する
        /// <summary>
        /// 翻訳一時表の情報一覧を取得する
        /// </summary>
        /// <param name="findWktTranslateDto">条件検索</param>
        /// <param name="wktTranslateService">奉仕</param>
        /// <returns>翻訳一時表の情報一覧</returns>
        private R<IEnumerable<WktTranslate>> FinManyHandler([AsParameters] FindWktTranslateRequestDto findWktTranslateDto, [FromServices] IWktTranslateService wktTranslateService)
        {
            var response = wktTranslateService.GetMany(findWktTranslateDto);
            return new R<IEnumerable<WktTranslate>>()
            {
                Payload = response
            };
        }
        #endregion
    }
}