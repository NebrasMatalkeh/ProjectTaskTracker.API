using BusinessLogic.Interfaces;
using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectTaskTracker.API.DataObjects;
using ProjectTaskTracker.API.Models;
using System.Security.Claims;

namespace ProjectTaskTracker.API.Controllers
{
    [Authorize(Roles = "Manager")]
    [ApiController]
    [Route("api/user")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AppDbContext _context;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("developers")]
        public async Task<IActionResult> GetAllDevelopers()
        {
            var developers = await _userService.GetAllDevelopers();
            return Ok(developers);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<UserDTO>> GetMyProfile()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = await _context.Users.FindAsync(userId);
            

            if (user == null || !user.IsActive)
                return NotFound();

            return new UserDTO
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role.ToString()

            };


        }

        [Authorize]
        [HttpPut("me")]
        public async Task<IActionResult> UpdateMyProfile(UpdateUserDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = await _context.Users.FindAsync(userId);

            if (user == null || !user.IsActive)
                return NotFound();

            user.FullName = dto.FullName;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [Authorize(Roles = "Manager")]
        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            user.IsActive = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}