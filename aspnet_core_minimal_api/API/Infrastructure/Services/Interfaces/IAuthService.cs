using Core.Entities;
using Core.Models;

namespace Infrastructure.Services;
public interface IAuthService
{
    LoginResponseDto Login(string email, string password);
    void Logout();

    LoginResponseDto GetMe();
}
