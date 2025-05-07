// AnnouncementsController.cs
using bookSansar.DTO;
using bookSansar.Services;
using Microsoft.AspNetCore.Mvc;

namespace bookSansar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementsController : ControllerBase
    {
        private readonly IAnnouncementService _service;

        public AnnouncementsController(IAnnouncementService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveAnnouncements()
        {
            var announcements = await _service.GetActiveAnnouncementsAsync();
            return Ok(announcements);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnnouncement([FromBody] AnnouncementCreateDto dto)
        {
            var announcement = await _service.CreateAnnouncementAsync(dto);
            return CreatedAtAction(nameof(GetAnnouncementById), new { id = announcement.Id }, announcement);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnnouncementById(Guid id)
        {
            var announcement = await _service.GetAnnouncementByIdAsync(id);
            return announcement == null ? NotFound() : Ok(announcement);
        }

        // Add other endpoints...
    }
}