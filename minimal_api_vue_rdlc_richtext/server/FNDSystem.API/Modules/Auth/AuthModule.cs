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

        #region ���[�U�̐V�K�쐬

        /// <summary>
        /// ���[�U�̐V�K�쐬
        /// </summary>
        /// <param name="user">���[�U�}�X�^</param>
        /// <param name="authService">�F�؃T�[�r�X</param>
        /// <returns></returns>
        private IResult CreateUser(CreateUserDto user, [FromServices] IAuthService authService)
        {
            authService.CreateUser(user.UserName, user.Password);
            return Results.Ok();
        }

        #endregion

        #region ���O�C���戵��
        /// <summary>
        /// ���O�C���戵��
        /// </summary>
        /// <param name="dto">���O�C�����N�G�X�g</param>
        /// <param name="authService">�F�؃T�[�r�X</param>
        /// <param name="validator">�o���f�[�^�[</param>
        /// <returns>���[�U�}�X�^</returns>
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

        #region ���[�U���̎擾
        /// <summary>
        /// ���[�U���̎擾
        /// </summary>
        /// <param name="authService">�F�؃T�[�r�X</param>
        /// <returns>���[�U�}�X�^</returns>
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