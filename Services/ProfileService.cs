// Services/ProfileService.cs
using System;
using System.Threading.Tasks;
using bookSansar.DTO.Auth;
using bookSansar.Entities;
using bookSansar.Repositories;

namespace bookSansar.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUserRepository _userRepository;

        public ProfileService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetProfile(Guid userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }

        public async Task<User> UpdateProfile(Guid userId, ProfileDTO profileDto)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
                throw new Exception("User not found");

            // Update only the properties that were provided
            if (!string.IsNullOrEmpty(profileDto.FirstName))
                user.FirstName = profileDto.FirstName;

            if (!string.IsNullOrEmpty(profileDto.LastName))
                user.LastName = profileDto.LastName;

            if (!string.IsNullOrEmpty(profileDto.Email))
                user.Email = profileDto.Email;

            await _userRepository.UpdateUserAsync(user);
            return user;
        }
    }
}