// Controllers/ProfileController.cs
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using bookSansar.DTO.Auth;
using bookSansar.Services;
using System.Security.Claims;

namespace bookSansar.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = await _profileService.GetProfile(userId);
            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromBody] ProfileDTO profileDto)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = await _profileService.UpdateProfile(userId, profileDto);
            return Ok(user);
        }
    }
}