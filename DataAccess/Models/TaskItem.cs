
using System.ComponentModel.DataAnnotations;

namespace ProjectTaskTracker.API.Models
{
    public enum TaskStatus {
        [Display(Name = "New")]
        New=0,
        [Display(Name = "In Progress")]
        InProgress=1,
        [Display(Name = "Completed")]
        Completed=2,
    }
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int? AssignedUserId { get; set; }
        public User AssignedUser { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
