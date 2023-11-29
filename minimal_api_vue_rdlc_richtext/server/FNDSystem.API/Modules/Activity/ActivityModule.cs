using FNDSystem.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Modules.Shared;

namespace FNDSystem
{
    public class ActivityModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/activity/list/{plantName}", GetListByPlantName)
                     .Produces<IEnumerable<Activity>>(200);

            return endpoints;
        }

        #region アクティビティ一覧の取得
        /// <summary>
        /// アクティビティ一覧の取得
        /// </summary>
        /// <param name="plantName">検索条件</param>
        /// <param name="activityService">奉仕</param>
        /// <returns>「観察対象マスタ」リスト</returns>
        private R<IEnumerable<Activity>?> GetListByPlantName(string plantName, [FromServices] IActivityService activityService)
        {
            var response = activityService.GetActivityList(plantName);

            return new R<IEnumerable<Activity>?>()
            {
                Payload = response
            };
        }
        #endregion
    }
}