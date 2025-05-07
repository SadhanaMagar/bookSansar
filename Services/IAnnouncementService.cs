using bookSansar.DTO;


namespace bookSansar.Services
{
    public interface IAnnouncementService
    {
        Task<AnnouncementDto> CreateAnnouncementAsync(AnnouncementCreateDto dto);
        Task<IEnumerable<AnnouncementDto>> GetActiveAnnouncementsAsync();
        Task<IEnumerable<AnnouncementDto>> GetAllAnnouncementsAsync();
        Task<AnnouncementDto?> GetAnnouncementByIdAsync(Guid id);
        Task<bool> UpdateAnnouncementAsync(Guid id, AnnouncementCreateDto dto);
        Task<bool> ToggleAnnouncementStatusAsync(Guid id);
        Task<bool> DeleteAnnouncementAsync(Guid id);
    }
}
