using BusinessLogic.DataTransferObjects;
using BusinessLogic.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using ProjectTaskTracker.API.DataObjects;
using ProjectTaskTracker.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;
        public DashboardService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<DashboardDto> GetDashboardDataAsync()
        {
            var dashboard = new DashboardDto();

            
            dashboard.TotalOpenTasks = await _context.Tasks
                .CountAsync(t => t.Status != ProjectTaskTracker.API.Models.TaskStatus.Completed);  

           
            dashboard.TasksByStatus = await _context.Tasks
                .GroupBy(t => t.Status)
                .Select(g => new
                {
                    Status = g.Key,
                    Count = g.Count()
                })
                .ToDictionaryAsync(g => g.Status.ToString(), g => g.Count);

            dashboard.TopDevelopers = await _context.Users
                .Where(u => u.Role == UserRole.Developer)
                .Select(u => new DeveloperProductivityDto
                {
                    DeveloperId = u.Id,
                    DeveloperName = u.FullName,
                    CompletedTasks = u.Tasks.Count(t => t.Status == ProjectTaskTracker.API.Models.TaskStatus.Completed)
                })
                .OrderByDescending(d => d.CompletedTasks)
                .Take(2)
                .ToListAsync();

            
            dashboard.OverdueProjects = await _context.Tasks
                .Where(t => t.DueDate < DateTime.Now && t.Status != ProjectTaskTracker.API.Models.TaskStatus.Completed) 
                .GroupBy(t => t.Project.Name)
                .Select(g => new OverdueProjectsDto
                {
                    ProjectName = g.Key,
                    OldestOverdueDate = g.Min(t => t.DueDate),
                    OverdueTasksCount = g.Count()
                })
                .OrderByDescending(p => p.OverdueTasksCount)
                .ToListAsync();

            return dashboard;
        }
    }





}
    
