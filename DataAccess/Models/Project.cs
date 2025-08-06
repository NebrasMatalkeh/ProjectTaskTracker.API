namespace ProjectTaskTracker.API.Models
{
    public class Project
    {
       
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public List<TaskItem> Tasks { get; set; }

    }
}
