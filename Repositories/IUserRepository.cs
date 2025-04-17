using System;
using System.Threading.Tasks;
using bookSansar.Entities;

namespace bookSansar.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByIdAsync(Guid id);
        Task UpdateUserAsync(User user);
        Task<bool> UserExists(string username, string email);
    }
}