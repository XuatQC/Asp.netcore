using FluentValidation;
using FluentValidation.Results;
using FNDSystem.Core.Dto;
using FNDSystem.Core.Dto.Auth;
using FNDSystem.Core.Filters;
using FNDSystem.Core.ServicesContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.Shared;

namespace FNDSystem
{
    public class AuthModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/auth/login", LoginHandler).Produces<LoginResponseDto>(200).WithName("Login").WithTags("Auth");
            endpoints.MapPost("/auth/create-user", CreateUser).Produces<CreateUserDto>(200).WithName("CreateAccount").WithTags("Auth");
            endpoints.MapGet("/auth/me", GetMeHandler).Produces<LoginResponseDto>(200).AddEndpointFilter<AuthFilter>().WithName("GetMe").WithTags("Auth");
            return endpoints;
        }

        #region ユーザの新規作成

        /// <summary>
        /// ユーザの新規作成
        /// </summary>
        /// <param name="user">ユーザマスタ</param>
        /// <param name="authService">認証サービス</param>
        /// <returns></returns>
        private IResult CreateUser(CreateUserDto user, [FromServices] IAuthService authService)
        {
            authService.CreateUser(user.UserName, user.Password);
            return Results.Ok();
        }

        #endregion

        #region ログイン取扱者
        /// <summary>
        /// ログイン取扱者
        /// </summary>
        /// <param name="dto">ログインリクエスト</param>
        /// <param name="authService">認証サービス</param>
        /// <param name="validator">バリデーター</param>
        /// <returns>ユーザマスタ</returns>
        private IResult LoginHandler(LoginRequestDto dto, [FromServices] IAuthService authService, IValidator<LoginRequestDto> validator)
        {
            ValidationResult result = validator.Validate(dto);

            if (result.IsValid)
            {
                var response = authService.Login(dto.Initial, dto.Pass,dto.PlantName );

                return Results.Ok(new R<LoginResponseDto>()
                {
                    Payload = response
                });
            }
            else
            {
                return Results.BadRequest(new R<LoginResponseDto>()
                {
                    Code = 400,
                    Message = result.Errors[0].ErrorMessage,
                });
            }
        }

        #endregion

        #region ユーザ情報の取得
        /// <summary>
        /// ユーザ情報の取得
        /// </summary>
        /// <param name="authService">認証サービス</param>
        /// <returns>ユーザマスタ</returns>
        [AllowAnonymous]
        private IResult GetMeHandler([FromServices] IAuthService authService)
        {
            LoginResponseDto loginResponseDto = authService.GetMe();

            return Results.Ok(new R<LoginResponseDto>()
            {
                Payload = loginResponseDto
            });
        }

        #endregion
    }
}