using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTaskTracker.API.DataObjects;
using System.Security.Claims;

namespace ProjectTaskTracker.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            try
            {
                var response = await _authService.Login(loginDTO);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("register-developer")]
        public async Task<IActionResult> RegisterDeveloper(RegisterDTO registerDTO)
        {
            try
            {
                var managerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var developer = await _authService.RegisterDeveloper(registerDTO, managerId);
                return Ok(developer);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

