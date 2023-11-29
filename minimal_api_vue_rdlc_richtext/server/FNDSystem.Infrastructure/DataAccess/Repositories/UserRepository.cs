using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.Dto;
using Microsoft.EntityFrameworkCore;

namespace FNDSystem.Infrastructure.DataAccess.Repositories;
public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(DBMasterContext masterDBContext, DBSlaveContext slaveDBContext) : base(masterDBContext, slaveDBContext)
    {

    }

    #region ログインを確認してください
    /// <summary>
    /// ログインを確認してください
    /// </summary>
    /// <param name="initial"></param>
    /// <param name="plantName"></param>
    /// <returns></returns>
    public User? IsExistUser(string initial, string plantName)
    {
        if (slaveDBContext.ReviewMembers == null) return null;
        var result = slaveDBContext?.Users?.Join(slaveDBContext.ReviewMembers, u => u.Initial, r => r.Initial, (u, r) => new
        {
            User = u,
            plantName = r.PlantName,
        }).Where(x => x.User.Initial == initial && x.plantName == plantName).FirstOrDefault();

        if (result == null)
        {
            return null;
        }
        return result.User;
    }
    #endregion

    #region ユーザ名取得
    /// <summary>
    /// ユーザ名取得
    /// </summary>
    /// <param name="findUserDto"></param>
    /// <returns></returns>
    public UserResponseDto? GetUserName(FindUserRequestDto findUserDto)
    {
        var result = this.slaveDBContext?.UserResponseDtos?
                         .FromSql($@" SELECT tbl_user.name, tbl_user.roma_name FROM tbl_ReviewMember 
                                      INNER JOIN tbl_user ON tbl_reviewmember.initial = tbl_user.initial
                                      WHERE plant_name = {findUserDto.PlantName} and tbl_ReviewMember.initial = {findUserDto.Initial} limit 1;")
                         .ToList();
        if (result != null && result.Count > 0)
        {
            return result.FirstOrDefault();
        }
        else
        {
            result = this.slaveDBContext?.UserResponseDtos?
                         .FromSql($@" SELECT name, roma_name FROM tbl_User WHERE initial = {findUserDto.Initial} limit 1;")
                         .ToList();
            return result?.FirstOrDefault();
        }
    }
    #endregion
}