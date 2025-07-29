namespace ProjectTaskTracker.API.DataObjects
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
    }

    public class ProjectCreateDTO
    {
        public string Name { get; set; }
    }
}
