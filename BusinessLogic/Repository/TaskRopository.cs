using Microsoft.EntityFrameworkCore;
using ProjectTaskTracker.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class TaskRopository
    {
        private readonly DbContext _context;
        public TaskRopository(DbContext context)
        {
            _context = context;
        }
        public async Task<List<TaskItem>> GetTasksByProjectAsync(int projectId, string? status = null)
        {
            var query = _context.Set<TaskItem>()
               .Where(t => t.ProjectId == projectId);
            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(t => t.Status.ToString() == status);
            }
            return await query.ToListAsync();
        }


       
    }
}
