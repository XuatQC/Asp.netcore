using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;
using FNDSystem.Core.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using Modules.Shared;

namespace FNDSystem
{
    public class ObsConclusionModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/obs-conclusion/list/{num}", GetObsConclusionListByNum)
                     .Produces<IEnumerable<ObsConclusionResponseDto>>(200);

            endpoints.MapGet("/obs-conclusion/{num}", GetObsConclusionByNum)
                     .Produces<ObsConclusion>(200);

            return endpoints;
        }

        #region 番号によりOBS添付ファイル（写真）一覧を取得する
        /// <summary>
        /// 番号によりOBS添付ファイル（写真）一覧を取得する
        /// </summary>
        /// <param name="num">番号</param>
        /// <param name="obsConclusionService">奉仕</param>
        /// <returns>OBS添付ファイル（写真）一覧</returns>
        private R<IEnumerable<ObsConclusion>> GetObsConclusionListByNum(string num, [FromServices] IObsConclusionService obsConclusionService)
        {
            var response = obsConclusionService.GetObsConclusionListByNum(num);
            return new R<IEnumerable<ObsConclusion>>()
            {
                Payload = response
            };
        }
        #endregion

        #region 番号によりOBS結論一覧を取得する
        /// <summary>
        /// 番号によりOBS結論を取得する
        /// </summary>
        /// <param name="num">番号</param>
        /// <param name="obsConclusionService">奉仕</param>
        /// <returns>OBS結論</returns>
        private R<ObsConclusion> GetObsConclusionByNum(string num, [FromServices] IObsConclusionService obsConclusionService)
        {
            var response = obsConclusionService.GetObsConclusionByNum(num);
            return new R<ObsConclusion>()
            {
                Payload = response
            };
        }
        #endregion

    }
}