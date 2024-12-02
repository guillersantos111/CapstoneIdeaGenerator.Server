using CapstoneIdeaGenerator.Server.Entities.DTOs;
using CapstoneIdeaGenerator.Server.Entities.Models;
using CapstoneIdeaGenerator.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneIdeaGenerator.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityLogsContoller : ControllerBase
    {
        private readonly IActivityLogsService activityLogsService;

        public ActivityLogsContoller(IActivityLogsService activityLogsService)
        {
            this.activityLogsService = activityLogsService;
        }


        [HttpPost("log")]
        public async Task<IActionResult> LogActivity([FromBody] ActivityLogsDTO request)
        {
            await activityLogsService.AddActivityLogs(request);
            return Ok();
        }


        [HttpGet("getlogs")]
        public async Task<ActionResult<IEnumerable<ActivityLogsDTO>>> GetActivityLogs()
        {
            var logs = await activityLogsService.GetAllActivityLogs();
            return Ok(logs);
        }
    }
}
