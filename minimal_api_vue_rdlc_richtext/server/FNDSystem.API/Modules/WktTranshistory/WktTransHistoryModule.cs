using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;
using FNDSystem.Core.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using Modules.Shared;

namespace FNDSystem
{
    public class WktTransHistoryModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            var wktTranHis = endpoints.MapGroup("/wkt-trans-history");

            wktTranHis.MapDelete("/", RemoveAll)
                      .Produces<bool>(200);

            wktTranHis.MapGet("/list", FinManyHandler)
                      .Produces<IEnumerable<WktTranslate>>(200);

            wktTranHis.MapGet("/process-data", ProcessDataHandler)
                      .Produces<IEnumerable<bool>>(200);

            return endpoints;
        }

        #region データ削除
        /// <summary>
        /// データ削除
        /// </summary>
        /// <param name="wktTransHistoryService">奉仕</param>
        /// <returns>true: 成功します。 false: 失敗します</returns>
        private IResult RemoveAll([FromServices] IWktTransHistoryService wktTransHistoryService)
        {
            var response = wktTransHistoryService.RemoveAll();

            return Results.Ok(new R<bool>()
            {
                Payload = response
            });
        }
        #endregion

        #region 翻訳履歴一覧を取得する
        /// <summary>
        /// 翻訳履歴一覧を取得する
        /// </summary>
        /// <param name="findWktTranslateHisDto">条件検索</param>
        /// <param name="wktTranslateHisService">奉仕</param>
        /// <returns>翻訳履歴一覧</returns>
        private R<IEnumerable<WktTransHistory>> FinManyHandler([AsParameters] FindWktTransHistoryRequest findWktTranslateHisDto, [FromServices] IWktTransHistoryService wktTranslateHisService)
        {
            var response = wktTranslateHisService.GetMany(findWktTranslateHisDto);
            return new R<IEnumerable<WktTransHistory>>()
            {
                Payload = response
            };
        }
        #endregion

        #region 履歴一時表のデータを処理する
        /// <summary>
        /// 履歴一時表のデータを処理する
        /// </summary>
        /// <param name="wktTransHistoryRequest">翻訳履歴データのリクエスト</param>
        /// <param name="wktTranslateHisService">奉仕</param>
        /// <returns>true: 成功します。 false: 失敗します</returns>
        private R<bool> ProcessDataHandler([AsParameters] WktTransHistoryRequest wktTransHistoryRequest, [FromServices] IWktTransHistoryService wktTranslateHisService)
        {
            var response = wktTranslateHisService.ProcessDataHandler(wktTransHistoryRequest);
            return new R<bool>()
            {
                Payload = response
            };
        }

        #endregion
    }
}