using FluentValidation;
using FluentValidation.Results;
using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;
using FNDSystem.Core.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using Modules.Shared;

namespace FNDSystem
{
    public class UserModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            return services;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/user/get-user-name", GetUserName)
                     .Produces<UserResponseDto>(200);

            endpoints.MapGet("/user/list", GetUserList)
                     .Produces<IEnumerable<User>>(200);

            endpoints.MapPut("/user/", UpdateByMul)
                     .Produces<bool>(200);
            return endpoints;
        }

        #region ユーザ一覧を取得する
        /// <summary>
        /// ユーザ一覧を取得する
        /// </summary>
        /// <param name="findUserDto">検索条件</param>
        /// <param name="userService">奉仕</param>
        /// <returns>ユーザ</returns>
        private R<IEnumerable<User>> GetUserList([AsParameters] FindUserRequestDto findUserDto, [FromServices] IUserService userService)
        {
            var response = userService.GetUserList(findUserDto);
            return new R<IEnumerable<User>>()
            {
                Payload = response
            };
        }
        #endregion

        #region ユーザ名を取得する
        /// <summary>
        /// ユーザ名を取得する
        /// </summary>
        /// <param name="findUserDto">検索条件</param>
        /// <param name="userService">奉仕</param>
        /// <returns>ユーザ</returns>
        private R<UserResponseDto?> GetUserName([AsParameters] FindUserRequestDto findUserDto, [FromServices] IUserService userService)
        {
            var response = userService.GetUserName(findUserDto);
            return new R<UserResponseDto?>()
            {
                Payload = response
            };
        }
        #endregion

        #region 複数条件でユーザを更新する
        /// <summary>
        /// 複数条件でユーザを更新する
        /// </summary>
        /// <param name="userDto">ユーザ</param>
        /// <param name="obsAttachService">奉仕</param>
        /// <param name="validator">バリデーター</param>
        /// <returns>true: 成功します。 false: 失敗します</returns>
        private IResult UpdateByMul(UserDto userDto, [FromServices] IUserService obsAttachService, IValidator<UserDto> validator)
        {
            ValidationResult result = validator.Validate(userDto);

            if (result.IsValid)
            {
                var response = obsAttachService.UpdateByMul(userDto);

                return Results.Ok(new R<bool>()
                {
                    Payload = response
                });
            }
            else
            {
                return Results.BadRequest(new R<bool>()
                {
                    Code = 400,
                    Message = result.Errors[0].ErrorMessage,
                });
            }
        }
        #endregion
    }
}