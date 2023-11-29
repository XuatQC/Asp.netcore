using FNDSystem.Core.Dto;
using FNDSystem.Core.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using Modules.Shared;

namespace FNDSystem.API.Modules
{
    public class PoAndCModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            return builder;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/po-and-c/list", GetPoAndCList)
                     .Produces<IEnumerable<PoAndCResponseDto>>(200);
            return endpoints;
        }

        /// <summary>
        /// PO&C一覧取得
        /// </summary>
        /// <param name="requestHeaderDto">検索条件</param>
        /// <param name="poAndCService">奉仕</param>
        /// <returns>達成目標（PO&C）マスタ一覧</returns>
        private R<IEnumerable<PoAndCResponseDto>?> GetPoAndCList([FromHeader] RequestHeaderDto requestHeaderDto, [FromServices] IPoAndCService poAndCService)
        {
            var response = poAndCService.GetPoAndCList(requestHeaderDto.PlantName);

            return new R<IEnumerable<PoAndCResponseDto>?>()
            {
                Payload = response
            };
        }
    }
}