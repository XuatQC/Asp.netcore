using AutoMapper;
using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.Dto;
using FNDSystem.Core.Extensions;
using FNDSystem.Core.Helpers;
using FNDSystem.Core.ServicesContracts;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace FNDSystem.Core.Services;
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


    /// <summary>
    /// �f�[�^�V�K�쐬
    /// </summary>
    /// <param name="accountDto">�f�[�^��}������</param>
    /// <returns>�V�����A�J�E���g</returns>
    public Account Create(CreateAccountRequestDto accountDto)
    {
        // �e�i���g�R�[�h�ɂ��t�B���^�[
        authenticatedUser = httpContextAccessor.HttpContext?.Items[nameof(AuthenticatedUser)] as AuthenticatedUser;
        var account = mapper.Map<Account>(accountDto);

        //�e�i���g�R�[�h�����O�C�����[�U�[�Ƀ��Z�b�g ���O�C�����[�U�[���Ǘ��҂łȂ��ꍇ
        if (authenticatedUser!.RoleKbn != RoleKbn.Administrator)
        {
            account.TenantCode = authenticatedUser.TenantCode;
        }

        account.Password = StringUtils.PasswordHash(account.Password!);
        unitOfWork.AccountRepository.Add(account);
        unitOfWork.SaveChanges();

        return account;
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="accountDto">��������</param>
    /// <returns>�A�J�E���g�ꗗ</returns>
    public IEnumerable<Account> FindMany(FindAccountRequestDto accountDto)
    {
        // �e�i���g�R�[�h�ɂ��t�B���^�[
        authenticatedUser = httpContextAccessor.HttpContext?.Items[nameof(AuthenticatedUser)] as AuthenticatedUser;
        Expression<Func<Account, bool>> whereExpression = acc => acc.TenantCode == authenticatedUser!.TenantCode;

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