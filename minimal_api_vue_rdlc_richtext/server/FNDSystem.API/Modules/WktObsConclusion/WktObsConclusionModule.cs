using FluentValidation;
using FluentValidation.Results;
using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;
using FNDSystem.Core.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using Modules.Shared;

namespace FNDSystem
{
    public class WktObsConclusionModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            var wktObsFact = endpoints.MapGroup("/wkt-obs-conclusion");

            wktObsFact.MapDelete("/{id}", DeleteById)
                      .Produces<bool>(200);

            wktObsFact.MapDelete("/", RemoveAll)
                      .Produces<bool>(200);

            wktObsFact.MapPost("/", CreateHandler)
                      .Produces<WktObsConclusion>(200);

            return endpoints;
        }

        #region IDで削除する
        /// <summary>
        /// IDで削除する
        /// </summary>
        /// <param name="id"></param>
        /// <param name="wktObsConclusionService">奉仕</param>
        /// <returns>true: 成功します。 false: 失敗します</returns>
        private IResult DeleteById(int id, [FromServices] IWktObsConclusionService wktObsConclusionService)
        {
            var response = wktObsConclusionService.DeleteById(id);

            return Results.Ok(new R<bool>()
            {
                Payload = response
            });
        }
        #endregion

        #region データ削除
        /// <summary>
        /// データ削除
        /// </summary>
        /// <param name="wktObsConclusionService">奉仕</param>
        /// <returns>true: 成功します。 false: 失敗します</returns>
        private IResult RemoveAll([FromServices] IWktObsConclusionService wktObsConclusionService)
        {
            var response = wktObsConclusionService.RemoveAll();

            return Results.Ok(new R<bool>()
            {
                Payload = response
            });
        }
        #endregion

        #region 新規作成
        /// <summary>
        /// 新規作成
        /// </summary>
        /// <param name="wktObsConDto">更新するデータ</param>
        /// <param name="wktObsConclusionService">奉仕</param>
        /// <param name="validator">バリデーター</param>
        /// <returns></returns>
        private IResult CreateHandler(WktObsConclusionDto wktObsConDto, [FromServices] IWktObsConclusionService wktObsConclusionService, IValidator<WktObsConclusionDto> validator)
        {
            ValidationResult result = validator.Validate(wktObsConDto);

            if (result.IsValid)
            {
                var response = wktObsConclusionService.Create(wktObsConDto);

                return Results.Ok(new R<WktObsConclusion>()
                {
                    Payload = response
                });
            }
            else
            {
                return Results.BadRequest(new R<WktObsConclusion>()
                {
                    Code = 400,
                    Message = result.Errors[0].ErrorMessage,
                });
            }
        }
        #endregion
    }
}