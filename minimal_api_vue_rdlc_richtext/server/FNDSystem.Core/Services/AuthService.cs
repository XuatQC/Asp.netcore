using AutoMapper;
using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.Dto;
using FNDSystem.Core.Exceptions;
using FNDSystem.Core.Helpers;
using FNDSystem.Core.ServicesContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace FNDSystem.Core.Services;

public class AuthService : GeneticService, IAuthService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IHttpContextAccessor httpContextAccessor;


    public AuthService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// ユーザの新規作成
    /// </summary>
    /// <param name="user">ユーザマスタ</param>
    /// <param name="password">パスワード</param>
    public void CreateUser(string user,string password)
    {
        if(!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(password))
        {
            var hashPassword = StringUtils.PasswordHash(password); 
            unitOfWork.UserRepository.Add(new User() { 
                Initial = user,
                Pass = hashPassword
            });
            unitOfWork.SaveChanges();
        }
    }

    /// <summary>
    /// ログイン
    /// </summary>
    /// <param name="email">メール</param>
    /// <param name="password">パスワード</param>
    /// <returns>user</returns>
    public LoginResponseDto Login(string initial, string password,string plantName)
    {
        User? user = unitOfWork.UserRepository.IsExistUser(initial, plantName);
        if (user is not null)
        {
            // パスワードをチェックする
            string hashPassword = user.Pass ?? string.Empty;
            if (StringUtils.VerifyPasswordHash(password, hashPassword))
            {
                string token = StringUtils.GenerateJWTToken(user);
                var loginResponseDto = new LoginResponseDto()
                {
                    Initial = user.Initial,
                    Token = token,
                    Language = user.Language,
                    Name = user.Name,
                    RomaName = user.RomaName,
                    RespnsArea = user.RespnsArea,
                };
                return loginResponseDto;
            }
            else
            {
                return new LoginResponseDto()
                {
                    Id = 0,
                    FieldError = "Password"
                };
            }
        }
        else
        {
            return new LoginResponseDto()
            {
                Id = 0,
                FieldError = "Initial"
            };
        }
    }

    /// <summary>
    /// ログアウト
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Logout()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// ユーザ情報の取得
    /// </summary>
    /// <returns>user</returns>
    /// <exception cref="UnauthorizedException"></exception>
    public LoginResponseDto GetMe()
    {
        var authenticatedUserVal = httpContextAccessor.HttpContext?.Items[nameof(AuthenticatedUserDto)] as AuthenticatedUserDto;
        Expression<Func<User, bool>> whereExp = acc => acc.Initial == authenticatedUserVal!.Initial;
        User? user= unitOfWork.UserRepository.Get(whereExp);

        if (user is not null)
        {
            user.Pass= string.Empty;
            var loginResponseDto = mapper.Map<LoginResponseDto>(user);
            string token = StringUtils.GenerateJWTToken(user);
            loginResponseDto.Token = token;
            return loginResponseDto;
        }
        throw new UnauthorizedException();
    }
}