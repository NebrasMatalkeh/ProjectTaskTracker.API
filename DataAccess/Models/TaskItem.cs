namespace ProjectTaskTracker.API.Models
{
    public enum TaskStatus { New , InProgress , Completed}
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int? AssignedDeveloperId { get; set; }
        public User AssignedDeveloper { get; set; }

    }
}
