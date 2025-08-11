
using DataAccess.RequestFeatures;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectTaskTracker.API.Models
{

    public enum TaskPriority
    {
        Low,
        Medium,
        High
    }

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
        public TaskPriority Priority { get; set; }  
        public DateTime? DueDate { get; set; }    

        //[NotMapped]

        // public MetaData MetaData { get; set; }
    }
}
