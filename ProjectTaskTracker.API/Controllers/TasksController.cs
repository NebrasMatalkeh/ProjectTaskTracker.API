using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTaskTracker.API.DataObjects;
using System.Security.Claims;

namespace ProjectTaskTracker.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskCreateDTO taskDTO)
        {
            try
            {
                var managerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var task = await _taskService.CreateTask(taskDTO, managerId);
                return Ok(task);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("assign")]
        public async Task<IActionResult> AssignTask(TaskAssignDTO assignDTO)
        {
            try
            {
                var managerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var task = await _taskService.AssignTask(assignDTO, managerId);
                return Ok(task);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Developer")]
        [HttpPut("{taskId}/status")]
        public async Task<IActionResult> UpdateTaskStatus(int taskId, TaskUpdateStatusDTO statusDTO)
        {
            try
            {
                var developerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var task = await _taskService.UpdateTaskStatus(taskId, statusDTO, developerId);
                return Ok(task);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [Authorize(Roles = "Developer")]
        [HttpGet("my-tasks")]
        public async Task<IActionResult> GetDeveloperTasks()
        {
            var developerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var tasks = await _taskService.GetDeveloperTasks(developerId);
            return Ok(tasks);
        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetProjectTasks(int projectId)
        {
            var tasks = await _taskService.GetProjectTasks(projectId);
            return Ok(tasks);
        }
    }
}
