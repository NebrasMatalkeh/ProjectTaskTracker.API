using ProjectTaskTracker.API.Models;

namespace ProjectTaskTracker.API.DataObjects
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string Status { get; set; }
        public string AssignedDeveloper { get; set; }
        public int ProjectId { get; set; }
       
    }
    public class TaskCreateDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public TaskPriority Priority { get; set; }

        public DateTime? DueDate { get; set; }
    }

    public class TaskAssignDTO
    {
        public int TaskId { get; set; }
        public int DeveloperId { get; set; }
    }

    public class TaskUpdateStatusDTO
    {
        public Models.TaskStatus Status { get; set; }
    }
}
