using BusinessLogic.Interfaces;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectTaskTracker.API.DataObjects;
using ProjectTaskTracker.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectTaskTracker.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
       private readonly ICustomAuthService _customAuthService;
        //private readonly UserManager<User> _userManager;
        //private readonly SignInManager<User> _signInManager;

        public AuthController(IAuthService authService , ICustomAuthService customAuthService)
            //UserManager<User> userManager,
            //SignInManager<User> signInManager)
        {
            _authService = authService;
            _customAuthService = customAuthService;
            //_userManager = userManager;
            //_signInManager = signInManager;

        }
           

           
        

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            try
            {
                var response = await _customAuthService.LoginAsync(loginDTO);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }

            //var user = await _userManager.FindByEmailAsync(loginDTO.Email);

            //if (user == null || !user.IsActive)
            //    return Unauthorized("Invalid email or password");

            //var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

            //if (!result.Succeeded)
            //    return Unauthorized("Invalid email or password");
            //var claims = new List<Claim>
            //    {
            //    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            //    new Claim(ClaimTypes.Email, user.Email),
            //    new Claim(ClaimTypes.Name, user.FullName),
            //    new Claim(ClaimTypes.Role, user.Role.ToString())
            //};
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ADFR34cDGwe48KccbfQLTM34BSFY64GF")); 
            //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //var token = new JwtSecurityToken(
            //    issuer: "TaskTracker65",
            //    audience: "TaskTracker87",
            //    claims: claims,
            //    expires: DateTime.UtcNow.AddHours(1),
            //    signingCredentials: creds
            //);
            //var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            //return Ok(new AuthResponseDTO
            //{
            //    Token = tokenString,
            //    Email = user.Email,
            //    FullName = user.FullName,
            //    Role = user.Role.ToString()
            //});










            //return new AuthResponseDTO
            //{


            //    Token = new JwtSecurityTokenHandler().WriteToken(token),
            //    Email = user.Email,
            //    FullName = user.FullName
            //};
            //try
            //{
            //    var response = await _authService.Login(loginDTO);
            //    return Ok(response);
            //}
            //catch (UnauthorizedAccessException ex)
            //{
            //    return Unauthorized(ex.Message);
            //}
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

