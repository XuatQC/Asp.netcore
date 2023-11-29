using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using Modules.Shared;

namespace FNDSystem
{
    public class ObsAttachModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/obs-attach/list/{num}", GetObsAttachListByImageCode)
                     .Produces<IEnumerable<ObsAttach>>(200);

            endpoints.MapGet("/obs-attach/{num}", GetObsAttachByImageCode)
                     .Produces<ObsAttach>(200);
            return endpoints;
        }

        #region 写真番号によりOBS添付ファイル（写真）一覧を取得する
        /// <summary>
        /// 写真番号によりOBS添付ファイル（写真）一覧を取得する
        /// </summary>
        /// <param name="num">番号</param>
        /// <param name="obsAttachService">奉仕</param>
        /// <returns>OBS添付ファイル（写真）リスト</returns>
        private R<IEnumerable<ObsAttach>> GetObsAttachListByImageCode(string num, [FromServices] IObsAttachService obsAttachService)
        {

            var response = obsAttachService.GetObsAttachListByImageCode(num);
            return new R<IEnumerable<ObsAttach>>()
            {
                Payload = response
            };
        }

        #endregion

        #region 写真番号によりOBS添付ファイル（写真）を取得する
        /// <summary>
        /// 写真番号によりOBS添付ファイル（写真）を取得する
        /// </summary>
        /// <param name="num">番号</param>
        /// <param name="obsAttachService">奉仕</param>
        /// <returns>OBS添付ファイル（写真）</returns>
        private R<ObsAttach> GetObsAttachByImageCode(string num, [FromServices] IObsAttachService obsAttachService)
        {
            var response = obsAttachService.GetObsAttachByImageCode(num);
            return new R<ObsAttach>()
            {
                Payload = response
            };
        }

        #endregion
    }
}