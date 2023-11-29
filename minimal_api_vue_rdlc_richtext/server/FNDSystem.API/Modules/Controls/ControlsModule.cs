using FNDSystem.Core.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using Modules.Shared;

namespace FNDSystem
{
    public class ControlsModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/controls/last-sync-date", GetLastSyncDate)
                     .Produces<IEnumerable<DateTime>>(200);

            return endpoints;
        }

        #region 最終同期日の取得
        /// <summary>
        /// 最終同期日の取得
        /// </summary>
        /// <param name="controlsService">奉仕</param>
        /// <returns>最終同期日</returns>
        private R<DateTime?> GetLastSyncDate([FromServices] IControlsService controlsService)
        {
            var response = controlsService.GetLastSyncDate();

            return new R<DateTime?>()
            {
                Payload = response
            };
        }
        #endregion
    }
}