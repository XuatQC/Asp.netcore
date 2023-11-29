using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using Modules.Shared;

namespace FNDSystem
{
    public class ObsFactPastModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/obs-fact-past/list/{num}", GetObsFactPartListByNum)
                     .Produces<IEnumerable<ObsFactPast>>(200);

            return endpoints;
        }

        #region 番号によりOBS観察事実（過去）一覧を取得する
        /// <summary>
        /// 番号によりOBS観察事実（過去）一覧を取得する
        /// </summary>
        /// <param name="num">番号</param>
        /// <param name="obsFactPastService">奉仕</param>
        /// <returns>OBS観察事実（過去）一覧</returns>
        private R<IEnumerable<ObsFactPast>> GetObsFactPartListByNum(string num, [FromServices] IObsFactPastService obsFactPastService)
        {
            var response = obsFactPastService.GetObsFactPastListByNum(num);
            return new R<IEnumerable<ObsFactPast>>()
            {
                Payload = response
            };
        }
        #endregion
    }
}