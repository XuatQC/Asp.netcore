using FluentValidation;
using FluentValidation.Results;
using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;
using FNDSystem.Core.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using Modules.Shared;

namespace FNDSystem
{
    public class WktObsFactModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            var wktObsFact = endpoints.MapGroup("/wkt-obs-fact");

            wktObsFact.MapDelete("/{id}", DeleteById)
                      .Produces<bool>(200);

            wktObsFact.MapDelete("/", RemoveAll)
                      .Produces<bool>(200);

            wktObsFact.MapPost("/", CreateHandler)
                      .Produces<WktObsFact>(200);

            return endpoints;
        }

        #region IDで削除する
        /// <summary>
        /// IDで削除する
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="obsScopeService">奉仕</param>
        /// <returns>true: 成功します。 false: 失敗します</returns>
        private IResult DeleteById(int id, [FromServices] IWktObsFactService obsScopeService)
        {
            var response = obsScopeService.DeleteById(id);

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
        /// <param name="obsScopeService">奉仕</param>
        /// <returns>true: 成功します。 false: 失敗します</returns>
        private IResult RemoveAll([FromServices] IWktObsFactService obsScopeService)
        {
            var response = obsScopeService.RemoveAll();

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
        /// <param name="wktObsFactDto">更新するデータ</param>
        /// <param name="wktObsFactService">奉仕</param>
        /// <param name="validator">バリデーター</param>
        /// <returns>true: 成功します。 false: 失敗します</returns>
        private IResult CreateHandler(WktObsFactDto wktObsFactDto, [FromServices] IWktObsFactService wktObsFactService, IValidator<WktObsFactDto> validator)
        {
            ValidationResult result = validator.Validate(wktObsFactDto);

            if (result.IsValid)
            {
                var response = wktObsFactService.Create(wktObsFactDto);

                return Results.Ok(new R<WktObsFact>()
                {
                    Payload = response
                });
            }
            else
            {
                return Results.BadRequest(new R<WktObsFact>()
                {
                    Code = 400,
                    Message = result.Errors[0].ErrorMessage,
                });
            }
        }
        #endregion
    }
}