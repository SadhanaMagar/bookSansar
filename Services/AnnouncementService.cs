using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bookSansar.DTO;
using bookSansar.Entities;
using bookSansar.Repositories;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace bookSansar.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<AnnouncementService> _logger;

        public AnnouncementService(
            IAnnouncementRepository repository,
            IMapper mapper,
            ILogger<AnnouncementService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<AnnouncementDto> CreateAnnouncementAsync(AnnouncementCreateDto dto)
        {
            try
            {
                // Manual mapping as fallback if AutoMapper fails
                var announcement = new Announcement
                {
                    Title = dto.Title,
                    Content = dto.Content,
                    EndDate = dto.EndDate,
                    // These are set automatically
                    StartDate = DateTime.UtcNow,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                var created = await _repository.CreateAsync(announcement);

                return new AnnouncementDto
                {
                    Id = created.Id,
                    Title = created.Title,
                    Content = created.Content,
                    StartDate = created.StartDate,
                    EndDate = created.EndDate,
                    IsActive = created.IsActive,
                    CreatedAt = created.CreatedAt
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating announcement");
                throw;
            }
        }

        public async Task<IEnumerable<AnnouncementDto>> GetActiveAnnouncementsAsync()
        {
            var announcements = await _repository.GetActiveAnnouncementsAsync();
            return _mapper.Map<IEnumerable<AnnouncementDto>>(announcements);
        }

        public async Task<IEnumerable<AnnouncementDto>> GetAllAnnouncementsAsync()
        {
            var announcements = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<AnnouncementDto>>(announcements);
        }

        public async Task<AnnouncementDto?> GetAnnouncementByIdAsync(Guid id)
        {
            var announcement = await _repository.GetByIdAsync(id);
            return announcement == null ? null : _mapper.Map<AnnouncementDto>(announcement);
        }

        public async Task<bool> UpdateAnnouncementAsync(Guid id, AnnouncementCreateDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            _mapper.Map(dto, existing);
            return await _repository.UpdateAsync(existing);
        }

        public async Task<bool> ToggleAnnouncementStatusAsync(Guid id)
        {
            var announcement = await _repository.GetByIdAsync(id);
            if (announcement == null) return false;

            announcement.IsActive = !announcement.IsActive;
            return await _repository.UpdateAsync(announcement);
        }

        public async Task<bool> DeleteAnnouncementAsync(Guid id)
        {
            try
            {
                return await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting announcement with ID {id}");
                throw;
            }
        }

        
    }
}