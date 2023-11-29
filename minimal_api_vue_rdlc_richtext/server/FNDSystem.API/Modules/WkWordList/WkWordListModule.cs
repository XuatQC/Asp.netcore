using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;
using FNDSystem.Core.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using Modules.Shared;
using FNDSystem;

namespace FNDSystem
{
    public class WkWordListModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            return builder;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            var wktObsFact = endpoints.MapGroup("/wk-word-list");

            wktObsFact.MapGet("/set-data-to-print", SetDataListToPrint)
                      .Produces<IEnumerable<PrintDataResponeseDto>>(200);

            wktObsFact.MapGet("/sort-data", SortDataList)
                          .Produces<WkWordList>(200);

            wktObsFact.MapDelete("/", RemoveAll)
                      .Produces<bool>(200);

            return endpoints;
        }

        #region データ削除
        /// <summary>
        /// データ削除
        /// </summary>
        /// <param name="wkWordListService">奉仕</param>
        /// <returns>true: 成功します。 false: 失敗します</returns>
        private IResult RemoveAll([FromServices] IWkWordListService wkWordListService)
        {
            var response = wkWordListService.RemoveAll();

            return Results.Ok(new R<bool>()
            {
                Payload = response
            });
        }
        #endregion

        #region 印刷データの準備
        /// <summary>
        /// 印刷データの準備
        /// </summary>
        /// <param name="printDto">印刷データ</param>
        /// <param name="wkWordListService">奉仕</param>
        /// <returns>印刷データ一覧</returns>
        private R<IEnumerable<PrintDataResponeseDto>?> SetDataListToPrint([AsParameters] FindPrintRequestDto printDto, [FromServices] IWkWordListService wkWordListService)
        {
            var response = wkWordListService.SetDataListToPrint(printDto);

            return new R<IEnumerable<PrintDataResponeseDto>?>()
            {
                Payload = response
            };
        }

        #endregion

        #region 一覧並び替え
        /// <summary>
        /// 一覧並び替え
        /// </summary>
        /// <param name="wkWordListService">奉仕</param>
        /// <returns>wkword一覧</returns>
        private R<IEnumerable<WkWordList>?> SortDataList([FromServices] IWkWordListService wkWordListService)
        {
            var response = wkWordListService.SortDataList();

            return new R<IEnumerable<WkWordList>?>()
            {
                Payload = response
            };
        }

        #endregion
    }
}