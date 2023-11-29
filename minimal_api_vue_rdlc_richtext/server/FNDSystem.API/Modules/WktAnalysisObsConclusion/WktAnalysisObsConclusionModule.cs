using FNDSystem.Core.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using Modules.Shared;
using FNDSystem;

namespace FNDSystem
{
    public class WktAnalysisObsConclusionModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            return builder;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            var wktObsFact = endpoints.MapGroup("/wkt-analysis-obs-conclusion");

            wktObsFact.MapDelete("/", RemoveAll)
                      .Produces<bool>(200);

            return endpoints;
        }

        #region データ削除
        /// <summary>
        /// データ削除
        /// </summary>
        /// <param name="wktAnalysisObsConclusionService">奉仕</param>
        /// <returns>true: 成功します。 false: 失敗します</returns>
        private IResult RemoveAll([FromServices] IWktAnalysisObsConclusionService wktAnalysisObsConclusionService)
        {
            var response = wktAnalysisObsConclusionService.RemoveAll();

            return Results.Ok(new R<bool>()
            {
                Payload = response
            });
        }
        #endregion
    }
}