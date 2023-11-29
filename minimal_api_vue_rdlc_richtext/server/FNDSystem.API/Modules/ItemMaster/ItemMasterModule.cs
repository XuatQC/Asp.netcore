using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;
using FNDSystem.Core.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using Modules.Shared;

namespace FNDSystem
{
    public class ItemMasterModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            var itemMaster = endpoints.MapGroup("/item-master");
            itemMaster.MapGet("/list", FindManyHandler).Produces<IEnumerable<ItemMasterModule>>(200);

            return endpoints;
        }

        #region 検索
        /// <summary>
        /// 検索
        /// </summary>
        /// <param name="findItemMasterDto">検索条件</param>
        /// <param name="findItemMasterDto">条件検索</param>
        /// <returns>アイテムマスタ一覧</returns>
        private R<IEnumerable<ItemMaster>> FindManyHandler([AsParameters] FindItemMasterRequestDto findItemMasterDto, [FromServices] IItemMasterService itemMasterService)
        {
            var response = itemMasterService.FindMany(findItemMasterDto);

            return new R<IEnumerable<ItemMaster>>()
            {
                Payload = response
            };
        }
        #endregion
    }
}