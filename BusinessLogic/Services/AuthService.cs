using BusinessLogic.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectTaskTracker.API.DataObjects;
using ProjectTaskTracker.API.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<AuthResponseDTO> Login(LoginDTO loginDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDTO.Email);
            if (user == null || Convert.ToBase64String(Encoding.UTF8.GetBytes(loginDTO.Password)) != user.PasswordHash)
                throw new UnauthorizedAccessException("Invalid credentials");

            var token = GenerateJWTToken(user);
            return new AuthResponseDTO { Token = token, Role = user.Role.ToString() };
        }

        public async Task<UserDTO> RegisterDeveloper(RegisterDTO registerDTO, int managerId)
        {
            if (await _context.Users.AnyAsync(u => u.Email == registerDTO.Email))
                throw new ArgumentException("Email already exists");

            var user = new User
            {
                FullName = registerDTO.FullName,
                Email = registerDTO.Email,
                PasswordHash = Convert.ToBase64String(Encoding.UTF8.GetBytes(registerDTO.Password)),
                Role = UserRole.Developer

            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDTO
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role.ToString()
            };
        }

        private string GenerateJWTToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

