namespace ProjectTaskTracker.API.DataObjects
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public TaskStatus Status { get; set; }
        public string AssignedDeveloper { get; set; }
        public string ProjectName { get; set; }
    }
    public class TaskCreateDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
    }

    public class TaskAssignDTO
    {
        public int TaskId { get; set; }
        public int DeveloperId { get; set; }
    }

    public class TaskUpdateStatusDTO
    {
        public TaskStatus Status { get; set; }
    }
}
