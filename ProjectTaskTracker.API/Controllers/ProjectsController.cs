using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTaskTracker.API.DataObjects;
using System.Security.Claims;

namespace ProjectTaskTracker.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateProject(ProjectCreateDTO projectDTO)
        {
            var managerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var project = await _projectService.CreateProject(projectDTO, managerId);
            return Ok(project);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _projectService.GetAllProjects();
            return Ok(projects);
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> GetProjectsAsync( string? searchTerm)
        {
         var projects = await _projectService.GetProjectsAsync(searchTerm);
          return Ok(projects);
        }
    }
}


