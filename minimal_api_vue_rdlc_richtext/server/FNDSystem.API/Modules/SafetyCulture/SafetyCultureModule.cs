using FNDSystem.Core.Dto;
using FNDSystem.Core.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using Modules.Shared;

namespace FNDSystem.API.Modules
{
    public class SafetyCultureModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            return builder;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/safety-culture/list", GetSafetyCultureList)
                     .Produces<IEnumerable<SafetyCultureResponseDto>>(200);
            return endpoints;
        }

        /// <summary>
        /// 安全文化一覧取得
        /// </summary>
        /// <param name="requestHeaderDto">検索条件</param>
        /// <param name="safetyCultureService">奉仕</param>
        /// <returns>安全文化一覧</returns>
        private R<IEnumerable<SafetyCultureResponseDto>?> GetSafetyCultureList([FromHeader] RequestHeaderDto requestHeaderDto, [FromServices] ISafetyCultureService safetyCultureService)
        {
            var response = safetyCultureService.GetSafetyCultureList(requestHeaderDto.PlantName);

            return new R<IEnumerable<SafetyCultureResponseDto>?>()
            {
                Payload = response
            };
        }
    }
}
