using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataTransferObjects
{
    public class DashboardDto
    {
        public int TotalOpenTasks { get; set; }

        public Dictionary<string, int> TasksByStatus { get; set; }
        public List<DeveloperProductivityDto> TopDevelopers { get; set; }
        public List<OverdueProjectsDto> OverdueProjects { get; set; }
    }

    public class DeveloperProductivityDto
    {
        public int DeveloperId { get; set; }
        public string DeveloperName { get; set; }
        public int CompletedTasks { get; set; }


    }

    public class OverdueProjectsDto
    {
        
        public string ProjectName { get; set; }
        public DateTime ? OldestOverdueDate{ get; set; }
        public int OverdueTasksCount { get; set; }
    }

}
