using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;

namespace FNDSystem.Core.Domain.RepositoryContracts;
public interface IUserRepository : IGenericRepository<User>
{
    UserResponseDto? GetUserName(FindUserRequestDto findUserDto);
    User? IsExistUser(string initial, string plantName);
}
