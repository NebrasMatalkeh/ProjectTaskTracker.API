using Microsoft.EntityFrameworkCore;
using ProjectTaskTracker.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class ProjectRepository
    {

        public ProjectRepository(DbContext context)
        {
            _context = context;
        }
        private readonly DbContext _context;
        public async Task<Project> GetProjectAsync(string? searchTerm)
        {

            var query = _context.Set<Project>().AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                //query = query.Where(p => EF.Functions.Like(p.Name, $"%{searchTerm}%"));
                // query = query.Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));

               query = query.Where(p => p.Name.Contains(searchTerm));

              
            }
            return await query.FirstOrDefaultAsync();


        }
    }
}
