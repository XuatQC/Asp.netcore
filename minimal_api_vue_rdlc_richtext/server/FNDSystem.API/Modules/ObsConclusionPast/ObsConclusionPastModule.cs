using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using Modules.Shared;

namespace FNDSystem
{
    public class ObsConclusionPastModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/obs-conclusion-past/list/{num}", GetObsConclusionPastListByNum)
                     .Produces<IEnumerable<ObsConclusionPast>>(200);

            return endpoints;
        }

        #region 番号によりOBS結論（過去）一覧を取得する

        /// <summary>
        /// 番号によりOBS結論（過去）一覧を取得する
        /// </summary>
        /// <param name="num">番号</param>
        /// <param name="obsConclusionPastService">奉仕</param>
        /// <returns>OBS結論（過去）一覧</returns>
        private R<IEnumerable<ObsConclusionPast>> GetObsConclusionPastListByNum(string num, [FromServices] IObsConclusionPastService obsConclusionPastService)
        {
            var response = obsConclusionPastService.GetObsConclusionPastListByNum(num);
            return new R<IEnumerable<ObsConclusionPast>>()
            {
                Payload = response
            };
        }
        #endregion
    }
}