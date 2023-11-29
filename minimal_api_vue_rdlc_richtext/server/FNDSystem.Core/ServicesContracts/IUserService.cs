using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;

namespace FNDSystem.Core.ServicesContracts;

public interface IUserService
{
    IEnumerable<User>? GetUserList(FindUserRequestDto findUserDto);
    UserResponseDto? GetUserName(FindUserRequestDto findUserDto);
    bool UpdateByMul(UserDto userDto);
}