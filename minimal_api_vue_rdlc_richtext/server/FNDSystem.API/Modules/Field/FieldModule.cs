using FNDSystem.Core.Domain;
using FNDSystem.Core.Dto;
using FNDSystem.Core.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using Modules.Shared;

namespace FNDSystem
{
    public class FieldModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/field/list", GetFieldList)
                     .Produces<IEnumerable<FieldResponseDto>>(200);

            endpoints.MapGet("/field/11-record", Get_11Records_Field)
                     .Produces<IEnumerable<Field>>(200);

            return endpoints;
        }

        #region 分野11件取得
        /// <summary>
        /// 分野11件取得
        /// </summary>
        /// <param name="findFieldDto">条件検索</param>
        /// <param name="fieldService">フィールド サービス</param>
        /// <returns> 分野11</returns>
        private R<IEnumerable<Field>> Get_11Records_Field([AsParameters] FindFieldRequestDto findFieldDto, [FromServices] IFieldService fieldService)
        {
            var response = fieldService.Get_11Records_Field(findFieldDto);

            return new R<IEnumerable<Field>>()
            {
                Payload = response
            };
        }
        #endregion

        #region 分野一覧取得
        /// <summary>
        /// 分野一覧取得
        /// </summary>
        /// <param name="findFieldDto">条件検索</param>
        /// <param name="fieldService">フィールド サービス</param>
        /// <returns>分野マスタリスト</returns>
        private R<IEnumerable<FieldResponseDto>> GetFieldList([AsParameters] FindFieldRequestDto findFieldDto, [FromServices] IFieldService fieldService)
        {
            var response = fieldService.GetFieldList(findFieldDto);

            return new R<IEnumerable<FieldResponseDto>>()
            {
                Payload = response
            };
        }
        #endregion

    }
}