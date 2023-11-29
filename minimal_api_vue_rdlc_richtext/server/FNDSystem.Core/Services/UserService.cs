using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.Dto;
using FNDSystem.Core.Extensions;
using FNDSystem.Core.ServicesContracts;
using System.Linq.Expressions;

namespace FNDSystem.Core.Services;
public class UserService : GeneticService, IUserService
{
    private readonly IUnitOfWork unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    /// <summary>
    /// ユーザ一覧を取得する
    /// </summary>
    /// <param name="findUserDto">条件検索</param>
    /// <returns> ユーザ一覧</returns>
    public IEnumerable<User>? GetUserList(FindUserRequestDto findUserDto)
    {
        Expression<Func<User, bool>> whereExpression = user => user.Initial == findUserDto.Initial;
        if (!string.IsNullOrEmpty(findUserDto.Pass))
        {
            Expression<Func<User, bool>> byPass = acc => acc.Pass == findUserDto.Pass;
            whereExpression = whereExpression.AndEx<User>(byPass);
        }

        Expression<Func<User, bool>> byValid = user => user.Valid;
        whereExpression = whereExpression.AndEx<User>(byValid);

        var result = unitOfWork.UserRepository.GetAll(whereExpression, 0, null);

        return result;
    }

    /// <summary>
    /// ユーザ名を取得する
    /// </summary>
    /// <param name="findUserDto">条件検索</param>
    /// <returns>ユーザ</returns>
    public UserResponseDto? GetUserName(FindUserRequestDto findUserDto)
    {

        var result = unitOfWork.UserRepository.GetUserName(findUserDto);

        return result;
    }

    #region 複数条件でユーザを更新する

    /// <summary>
    /// 複数条件でユーザを更新する
    /// </summary>
    /// <param name="userDto">ユーザ</param>
    /// <returns>true: 成功します。 false: 失敗します</returns>
    public bool UpdateByMul(UserDto userDto)
    {
        Expression<Func<User, bool>> whereExp = user => user.Valid;

        if (!string.IsNullOrEmpty(userDto.Initial))
        {
            Expression<Func<User, bool>> byInitial = item => item.Initial == userDto.Initial;
            whereExp = whereExp.AndEx<User>(byInitial);
        }

        if (!string.IsNullOrEmpty(userDto.Pass))
        {
            Expression<Func<User, bool>> byPass = acc => acc.Pass == userDto.Pass;
            whereExp = whereExp.AndEx<User>(byPass);
        }

        List<User> userDbList = unitOfWork.UserRepository.GetAll(whereExp, 0, null).ToList();

        if (userDbList == null || userDbList.Count == 0) return false;
        foreach (User userDb in userDbList)
        {
            userDb.Pass = userDto.Pass;
            userDb.Initial = userDto.Initial;
            userDb.Valid = true;

            unitOfWork.UserRepository.Update(userDb);
        }
        unitOfWork.SaveChanges();
        return true;
    }
    #endregion
}