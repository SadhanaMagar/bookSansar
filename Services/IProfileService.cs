using bookSansar.DTO.Auth;
using bookSansar.Entities;

namespace bookSansar.Services
{
    public interface IProfileService
    {
        Task<User> GetProfile(Guid userId);
        Task<User> UpdateProfile(Guid userId, ProfileDTO profileDto);
    }
}