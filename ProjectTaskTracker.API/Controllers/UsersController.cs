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
    [ApiController]
    [Route("api/user")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AppDbContext _context;

        public UsersController(IUserService userService, AppDbContext context)
        
        {
            _userService = userService;
            _context = context;
        }

        [HttpGet("developers")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetAllDevelopers()
        {
            var developers = await _userService.GetAllDevelopers();
            return Ok(developers);
        }

        
        [HttpGet("me")]
        [Authorize(Roles = "Developer")]
        public async Task<ActionResult<UserDTO>> GetMyProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);
           if (string.IsNullOrEmpty(userId?.Value))
                return Unauthorized("User not authenticated.");
           var user = await _context.FindAsync<User>(int.Parse(userId.Value));
            if (user == null || !user.IsActive)
                return NotFound("User not found or inactive.");
            return Ok(new UserDTO
                {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role.ToString()
            });


            //var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            //var user = await _context.Users
            //    .Where(u => u.Id == userId && u.IsActive)
            //    .Select(u => new UserDTO
            //    {
            //        Id = u.Id,
            //        FullName = u.FullName,
            //        Email = u.Email,
            //        Role = u.Role.ToString()
            //    })
            //    .FirstOrDefaultAsync();
            //if (user == null)
            //    return NotFound();
            //return Ok(user);




            //var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            //var user = await _context.Users.FindAsync(userId);


            //if (user == null || !user.IsActive)
            //    return NotFound();

            //return new UserDTO
            //{
            //    Id = user.Id,
            //    FullName = user.FullName,
            //    Email = user.Email,
            //    Role = user.Role.ToString()

            //};


        }

        [HttpPut("update")]
        [Authorize(Roles = "Developer")]
        public async Task<IActionResult> UpdateMyProfile(UpdateUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(string.IsNullOrEmpty(userId))
                return Unauthorized("User not authenticated.");
            var user = await _context.Users.FindAsync(int.Parse(userId));
            if (user == null || !user.IsActive)
                return NotFound("User not found or inactive.");
            user.FullName =dto.FullName;
            user.Email = dto.Email;
            user.PasswordHash = dto.Password;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return Ok(new UserDTO
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role.ToString()
            });








            //// Assuming you want to update the current user's profile based on their claims
            //var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            //var user = await _context.Users.FindAsync(userId);
            //if (user == null || !user.IsActive)
            //    return NotFound();
            //user.FullName = dto.FullName;
            //await _context.SaveChangesAsync();
            //return NoContent();

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