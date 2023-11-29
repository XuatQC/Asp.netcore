using System.Linq.Expressions;
using AutoMapper;
using Core.Entities;
using Core.Helpers;
using Core.Models;
using Core.Exceptions;
using Infrastructure.DataAccess.Repositories;

namespace Infrastructure.Services;

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

    public LoginResponseDto Login(string email, string password)
    {
        Expression<Func<Account, bool>> whereExp = acc => acc.Email == email;
        Account account = this.unitOfWork.AccountRepository.Get(whereExp);

        if (account is not null)
        {
            // check password
            string hashPassword = account.Password ?? string.Empty;
            if (StringUtils.VerifyPasswordHash(password, hashPassword))
            {
                account.Password = string.Empty;
                var loginResponseDto = mapper.Map<LoginResponseDto>(account);
                string token = StringUtils.GenerateJWTToken(account);
                loginResponseDto.Token = token;

                return loginResponseDto;
            }
        }

        throw new ControlledException("Thông tin đăng nhập không hợp lệ");
    }

    public void Logout()
    {
        throw new NotImplementedException();
    }

    public LoginResponseDto GetMe()
    {
        this.authenticatedUser = httpContextAccessor.HttpContext?.Items[nameof(AuthenticatedUser)] as AuthenticatedUser;
        Expression<Func<Account, bool>> whereExp = acc => acc.Code == this.authenticatedUser!.Code;
        Account account = this.unitOfWork.AccountRepository.Get(whereExp);

        if (account is not null)
        {
            account.Password = string.Empty;
            var loginResponseDto = mapper.Map<LoginResponseDto>(account);
            string token = StringUtils.GenerateJWTToken(account);
            loginResponseDto.Token = token;

            return loginResponseDto;
        }

        throw new UnauthorizedException();
    }
}