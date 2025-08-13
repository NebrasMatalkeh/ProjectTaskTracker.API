using ProjectTaskTracker.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
   public class Attachment
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadDate { get; set; }
        public int TaskItemId { get; set; }
        public TaskItem TaskItem { get; set; }
    }
}
