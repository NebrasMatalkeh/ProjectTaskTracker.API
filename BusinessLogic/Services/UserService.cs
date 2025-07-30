using BusinessLogic.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using ProjectTaskTracker.API.DataObjects;
using ProjectTaskTracker.API.Models;

namespace ProjectTaskTracker.API.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserDTO>> GetAllDevelopers()
        {
            return await _context.Users
                .Where(u => u.Role == UserRole.Developer)
                .Select(u => new UserDTO
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    Email = u.Email,
                    Role = u.Role
                })
                .ToListAsync();
        }
    }
}
