using ProjectTaskTracker.API.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
   public interface IProjectService
    {
        Task<ProjectDTO> CreateProject(ProjectCreateDTO projectDTO, int managerId);
        Task<IEnumerable<ProjectDTO>> GetAllProjects();
    }
}
