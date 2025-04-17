using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using bookSansar.Entities;

namespace bookSansar.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => EF.Functions.ILike(u.Username, username));
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => EF.Functions.ILike(u.Email, email));
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserExists(string username, string email)
        {
            return await _context.Users
                .AnyAsync(u => EF.Functions.ILike(u.Username, username) ||
                       EF.Functions.ILike(u.Email, email));
        }
    }
}