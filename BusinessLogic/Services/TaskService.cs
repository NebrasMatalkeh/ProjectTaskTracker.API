using BusinessLogic.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using ProjectTaskTracker.API.DataObjects;
using ProjectTaskTracker.API.Models;
using TaskStatus = ProjectTaskTracker.API.Models.TaskStatus;

namespace ProjectTaskTracker.API.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TaskDTO> CreateTask(TaskCreateDTO taskDTO, int managerId)
        {
            var project = await _context.Projects.FindAsync(taskDTO.ProjectId);
            if (project == null) throw new KeyNotFoundException("Project not found");

            var task = new TaskItem
            {
                Title = taskDTO.Title,
                Description = taskDTO.Description,
                CreationDate = DateTime.UtcNow,
                Status = TaskStatus.New,
                ProjectId = taskDTO.ProjectId
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return new TaskDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                CreationDate = task.CreationDate,
                Status = task.Status.ToString(),
                ProjectId = project.Id
            };
        }

        public async Task<TaskDTO> AssignTask(TaskAssignDTO assignDTO, int managerId)
        {
            var task = await _context.Tasks.FindAsync(assignDTO.TaskId);
            if (task == null) throw new KeyNotFoundException("Task not found");

            var developer = await _context.Users.FindAsync(assignDTO.DeveloperId);
            if (developer == null || developer.Role != UserRole.Developer)
                throw new ArgumentException("Invalid developer");

            task.AssignedUserId = assignDTO.DeveloperId;
            task.Status = TaskStatus.InProgress;
            await _context.SaveChangesAsync();

            return new TaskDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                CreationDate = task.CreationDate,
                Status =task.Status.ToString(),
                AssignedDeveloper = developer.FullName,
                ProjectId = task.ProjectId,
            };
        }

        public async Task<TaskDTO> UpdateTaskStatus(int taskId, TaskUpdateStatusDTO statusDTO, int developerId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task == null) throw new KeyNotFoundException("Task not found");
            if (task.AssignedUserId != developerId) throw new UnauthorizedAccessException("Not authorized to update this task");

            task.Status = (TaskStatus)statusDTO.Status;
            await _context.SaveChangesAsync();

            return new TaskDTO
            {
                Id = task.Id,
               
                Status = task.Status.ToString(),
               
            };
        }

        public async Task<IEnumerable<TaskDTO>> GetDeveloperTasks(int developerId)
        {
            return await _context.Tasks
                .Where(t => t.AssignedUserId == developerId)
                .Select(t => new TaskDTO
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    CreationDate = t.CreationDate,
                    Status = t.Status.ToString(),
                    ProjectId = t.Project.Id    
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskDTO>> GetProjectTasks(int projectId)
        {
            return await _context.Tasks
                .Where(t => t.ProjectId == projectId)
                .Select(t => new TaskDTO
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    CreationDate = t.CreationDate,
                    Status = t.Status.ToString(),
                    AssignedDeveloper = t.AssignedUser.FullName,
                    ProjectId = t.Project.Id    
                })
                .ToListAsync();
        }
    }
}