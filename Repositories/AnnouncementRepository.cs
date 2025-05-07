using bookSansar.Entities;

using Microsoft.EntityFrameworkCore;

namespace bookSansar.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly AppDBContext _context;

        public AnnouncementRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Announcement> CreateAsync(Announcement announcement)
        {
            await _context.Announcements.AddAsync(announcement);
            await _context.SaveChangesAsync();
            return announcement;
        }

        public async Task<IEnumerable<Announcement>> GetActiveAnnouncementsAsync()
        {
            var now = DateTime.UtcNow;
            return await _context.Announcements
                .Where(a => a.IsActive && a.StartDate <= now && a.EndDate >= now)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Announcement>> GetAllAsync()
        {
            return await _context.Announcements
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }

        public async Task<Announcement?> GetByIdAsync(Guid id)
        {
            return await _context.Announcements.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(Announcement announcement)
        {
            _context.Announcements.Update(announcement);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var announcement = await _context.Announcements.FindAsync(id);
            if (announcement == null) return false;

            _context.Announcements.Remove(announcement);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}