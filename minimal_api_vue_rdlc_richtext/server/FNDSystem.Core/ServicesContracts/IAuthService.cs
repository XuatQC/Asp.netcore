using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;

namespace FNDSystem.Core.ServicesContracts;
public interface IAuthService
{
    void CreateUser(string user, string password);
    LoginResponseDto Login(string initial, string password, string plantName);
    void Logout();
    LoginResponseDto GetMe();
}
