namespace ProjectTaskTracker.API.Models
{

    public enum UserRole { Manager , Developer}
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role{ get; set; }
        public List<TaskItem> Tasks { get; set; }

    }
}
