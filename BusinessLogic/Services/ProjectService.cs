using BusinessLogic.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using ProjectTaskTracker.API.DataObjects;
using ProjectTaskTracker.API.Models;

namespace ProjectTaskTracker.API.Services
{
    public class ProjectService : IProjectService
    {
        private readonly AppDbContext _context;

        public ProjectService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProjectDTO> CreateProject(ProjectCreateDTO projectDTO, int managerId)
        {
            var project = new Project
            {
                Name = projectDTO.Name,
                CreationDate = DateTime.UtcNow
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return new ProjectDTO
            {
                Id = project.Id,
                Name = project.Name,
                CreationDate = project.CreationDate
            };
        }

        public async Task<IEnumerable<ProjectDTO>> GetAllProjects()
        {
            return await _context.Projects
                .Select(p => new ProjectDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    CreationDate = p.CreationDate
                })
                .ToListAsync();
        }
    }
}
