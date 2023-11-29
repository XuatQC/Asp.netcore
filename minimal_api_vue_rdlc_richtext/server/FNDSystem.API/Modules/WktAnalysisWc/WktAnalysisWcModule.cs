using FNDSystem.Core.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using Modules.Shared;

namespace FNDSystem
{
    public class WktAnalysisWcModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            var wktObsFact = endpoints.MapGroup("/wkt-analysis-wc");

            wktObsFact.MapDelete("/", RemoveAll)
                      .Produces<bool>(200);

            return endpoints;
        }

        #region データ削除
        /// <summary>
        /// データ削除
        /// </summary>
        /// <param name="wktAnalysisWcService">奉仕</param>
        /// <returns>true: 成功します。 false: 失敗します</returns>
        private IResult RemoveAll([FromServices] IWktAnalysisWcService wktAnalysisWcService)
        {
            var response = wktAnalysisWcService.RemoveAll();

            return Results.Ok(new R<bool>()
            {
                Payload = response
            });
        }
        #endregion

    }
}