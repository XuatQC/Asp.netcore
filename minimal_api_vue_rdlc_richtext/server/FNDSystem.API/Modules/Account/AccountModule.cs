using FluentValidation;
using FluentValidation.Results;
using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;
using FNDSystem.Core.Filters;
using FNDSystem.Core.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using Modules.Shared;

namespace FNDSystem
{
    public class AccountModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            return builder;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            var account = endpoints.MapGroup("/accounts")
                            .AddEndpointFilter<AuthFilter>()
                            .WithTags("Accounts");
            account.MapGet("", FindManyHandler).RequireAuthorization("Owner").Produces<IEnumerable<Account>>(200);
            account.MapPost("", CreateHandler).RequireAuthorization("Owner").Produces<Account>(200);

            return endpoints;
        }

        #region 検索
        /// <summary>
        /// 検索
        /// </summary>
        /// <param name="findAccountDto">検索条件</param>
        /// <param name="accountService">奉仕</param>
        /// <returns>アカウント一覧</returns>
        private R<IEnumerable<Account>> FindManyHandler([AsParameters] FindAccountRequestDto findAccountDto, [FromServices] IAccountService accountService)
        {
            var response = accountService.FindMany(findAccountDto);

            return new R<IEnumerable<Account>>()
            {
                Payload = response
            };
        }
        #endregion

        #region データ新規作成
        /// <summary>
        /// データ新規作成
        /// </summary>
        /// <param name="accountDto">データを挿入する</param>
        /// <param name="accountService">奉仕</param>
        /// <param name="validator">バリデーター</param>
        /// <returns>新しいアカウント</returns>
        private IResult CreateHandler(CreateAccountRequestDto accountDto, [FromServices] IAccountService accountService, IValidator<CreateAccountRequestDto> validator)
        {
            ValidationResult result = validator.Validate(accountDto);

            if (result.IsValid)
            {
                var response = accountService.Create(accountDto);

                return Results.Ok(new R<Account>()
                {
                    Payload = response
                });
            }
            else
            {
                return Results.BadRequest(new R<Account>()
                {
                    Code = 400,
                    Message = result.Errors[0].ErrorMessage,
                });
            }
        }
        #endregion
    }
}