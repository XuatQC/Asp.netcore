using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;
using FNDSystem.Core.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using Modules.Shared;

namespace FNDSystem
{
    public class ObsFactModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/obs-fact/list/{num}", GetObsFactListByNum)
                     .Produces<IEnumerable<ObsFactResponseDto>>(200);

            endpoints.MapGet("/obs-fact/{num}", GetObsFactByNum)
                     .Produces<ObsFact>(200);

            return endpoints;
        }

        #region 番号によりOBS観察事実一覧を取得する
        /// <summary>
        /// 番号によりOBS観察事実一覧を取得する
        /// </summary>
        /// <param name="num">番号</param>
        /// <param name="obsFactService">奉仕</param>
        /// <returns>OBS観察事実一覧</returns>
        private R<IEnumerable<ObsFactResponseDto>> GetObsFactListByNum(string num, [FromServices] IObsFactService obsFactService)
        {
            var response = obsFactService.GetObsFactListByNum(num);
            return new R<IEnumerable<ObsFactResponseDto>>()
            {
                Payload = response
            };
        }
        #endregion

        #region 番号によりOBS観察事実を取得する
        /// <summary>
        /// 番号によりOBS観察事実を取得する
        /// </summary>
        /// <param name="num">番号</param>
        /// <param name="obsFactService">奉仕</param>
        /// <returns>OBS観察事実</returns>
        private R<ObsFact> GetObsFactByNum(string num, [FromServices] IObsFactService obsFactService)
        {
            var response = obsFactService.GetObsFactByNum(num);
            return new R<ObsFact>()
            {
                Payload = response
            };
        }
        #endregion

    }
}