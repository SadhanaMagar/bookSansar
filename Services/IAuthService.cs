// Services/IAuthService.cs
using System.Threading.Tasks;
using bookSansar.DTO.Auth;
using bookSansar.Entities;

namespace bookSansar.Services
{
    public interface IAuthService
    {
        Task<User> Register(RegisterDTO registerDto);
        Task<bool> Login(LoginDTO loginDto);
        Task Logout();
    }
}