using bookSansar.Entities;
namespace bookSansar.Repositories
{
    public interface IAnnouncementRepository
    {
        Task<Announcement> CreateAsync(Announcement announcement);
        Task<IEnumerable<Announcement>> GetActiveAnnouncementsAsync();
        Task<IEnumerable<Announcement>> GetAllAsync();
        Task<Announcement?> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(Announcement announcement);
        Task<bool> DeleteAsync(Guid id);
    }
}