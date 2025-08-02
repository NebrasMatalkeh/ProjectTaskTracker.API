using System.ComponentModel.DataAnnotations;

namespace ProjectTaskTracker.API.Models
{

    public enum UserRole {
        [Display(Name = "Manager")]
        Manager = 0,
        [Display(Name = "Developer")]
        Developer = 1,
    }
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }
        public List<TaskItem> Tasks { get; set; }

    }
}
