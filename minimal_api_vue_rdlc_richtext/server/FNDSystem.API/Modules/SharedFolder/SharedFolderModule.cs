using FNDSystem.API.Modules;
using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using Modules.Shared;

namespace FNDSystem
{
    public class SharedFolderModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/shared-folder/menu-list", GetSharedFolderMenu)
                     .Produces<IEnumerable<SharedFolder>>(200);

            endpoints.MapGet("/shared-folder/list", GetSharedFolderList)
                     .Produces<IEnumerable<SharedFolder>>(200);

            return endpoints;
        }

        #region フォルダ情報取得
        /// <summary>
        /// フォルダ情報取得
        /// </summary>
        /// <param name="requestHeaderDto">検索条件</param>
        /// <param name="shareFolderService">奉仕</param>
        /// <returns>共有フォルダ／保存先フォルダマスタ</returns>
        private R<IEnumerable<SharedFolder>> GetSharedFolderMenu([FromHeader] RequestHeaderDto requestHeaderDto, [FromServices] ISharedFolderService shareFolderService)
        {
            var response = shareFolderService.GetSharedFolderMenu(requestHeaderDto.PlantName);
            return new R<IEnumerable<SharedFolder>>()
            {
                Payload = response
            };
        }
        #endregion

        #region Pposによりフォルダ情報を取得する
        /// <summary>
        /// Pposによりフォルダ情報を取得する
        /// </summary>
        /// <param name="requestHeaderDto">検索条件</param>
        /// <param name="shareFolderService">奉仕</param>
        /// <returns>共有フォルダ／保存先フォルダマスタ一覧</returns>
        private R<IEnumerable<SharedFolder>> GetSharedFolderList([FromHeader] RequestHeaderDto requestHeaderDto, [FromServices] ISharedFolderService shareFolderService)
        {
            var response = shareFolderService.GetSharedFolderList(requestHeaderDto.PlantName);
            return new R<IEnumerable<SharedFolder>>()
            {
                Payload = response
            };
        }
        #endregion
    }
}