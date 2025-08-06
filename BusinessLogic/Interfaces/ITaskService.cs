using DataAccess.RequestFeatures;
using Microsoft.CodeAnalysis;
using ProjectTaskTracker.API.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
   public interface ITaskService
    {
        Task<TaskDTO> CreateTask(TaskCreateDTO taskDTO, int managerId);
        Task<TaskDTO> AssignTask(TaskAssignDTO assignDTO, int managerId);
        Task<TaskDTO> UpdateTaskStatus(int taskId, TaskUpdateStatusDTO statusDTO, int developerId);
        Task<IEnumerable<TaskDTO>> GetDeveloperTasks(int developerId);
      
        Task<IEnumerable<TaskDTO>> GetProjectTasks(int projectId);
        Task<(IEnumerable<TaskDTO> tasks, MetaData metaData) > GetProjectTasksWithPagination( TaskParameters taskParameters );
        //Task<IEnumerable<TaskDTO>> GetTasksByProjectAsync(int projectId, string? status);
        Task<IEnumerable<TaskDTO>> GetTasksByProjectAsync( int projectId , string? status);

    }
}
