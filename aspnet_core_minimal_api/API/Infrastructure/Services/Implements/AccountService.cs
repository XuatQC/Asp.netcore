using System.Linq.Expressions;
using AutoMapper;
using Core.Entities;
using Core.Helpers;
using Core.Models;
using Infrastructure.DataAccess.Repositories;
using Core.Extensions;
using Core.Helpers.Enum;

namespace Infrastructure.Services;
public class AccountService : GeneticService, IAccountService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IHttpContextAccessor httpContextAccessor;

    public AccountService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.httpContextAccessor = httpContextAccessor;
    }

    public Account Create(CreateAccountRequestDto accountDto)
    {
        // Filter by TenantCode
        this.authenticatedUser = httpContextAccessor.HttpContext?.Items[nameof(AuthenticatedUser)] as AuthenticatedUser;
        var account = mapper.Map<Account>(accountDto);

        // Reset Tenant Code to logged-in user If logged-in user is not Administrator
        if (this.authenticatedUser!.RoleKbn != RoleKbn.Administrator)
        {
            account.TenantCode = this.authenticatedUser.TenantCode;
        }

        account.Password = StringUtils.PasswordHash(account.Password!);
        this.unitOfWork.AccountRepository.Add(account);
        this.unitOfWork.Commit();

        return account;
    }

    public IEnumerable<Account> FindMany(FindAccountRequestDto accountDto)
    {
        // Filter by TenantCode
        this.authenticatedUser = httpContextAccessor.HttpContext?.Items[nameof(AuthenticatedUser)] as AuthenticatedUser;
        Expression<Func<Account, bool>> whereExpression = acc => acc.TenantCode == this.authenticatedUser!.TenantCode;

        if (!string.IsNullOrEmpty(accountDto.Email))
        {
            Expression<Func<Account, bool>> byEmail = acc => acc.Email == accountDto.Email;
            whereExpression = whereExpression.AndEx<Account>(byEmail);
        }

        if (!string.IsNullOrEmpty(accountDto.Code))
        {
            Expression<Func<Account, bool>> byCode = acc => acc.Code == accountDto.Code;
            whereExpression = whereExpression.AndEx<Account>(byCode);
        }

        var accounts = unitOfWork.AccountRepository.GetAll(whereExpression, accountDto.PageNo, new string[] { "Tenant" });

        return accounts;
    }
}