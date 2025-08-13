using BusinessLogic.DataTransferObjects;
using BusinessLogic.Interfaces;
using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProjectTaskTracker.API.Controllers
{
   
    [ApiController]
    [Route("api/dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<DashboardDto>> GetDashboardData()
        {
            var dashboardData = await _dashboardService.GetDashboardDataAsync();
            if (dashboardData == null)
            {
                return NotFound("Dashboard data not found.");
            }
            return Ok(dashboardData);
        }

    }
}