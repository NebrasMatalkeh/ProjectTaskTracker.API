using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    }
}