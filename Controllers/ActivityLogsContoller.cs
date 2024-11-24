using CapstoneIdeaGenerator.Server.Entities.DTOs;
using CapstoneIdeaGenerator.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneIdeaGenerator.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivityLogsController : ControllerBase
    {
        private readonly IActivityLogsService activityLogsService;

        public ActivityLogsController(IActivityLogsService activityLogsService)
        {
            this.activityLogsService = activityLogsService;
        }

        [HttpPost("post")]
        public async Task<IActionResult> LogActivity([FromBody] ActivityLogsDTO activityLogDTO)
        {
            await activityLogsService.LogActivity(activityLogDTO);
            return Ok();
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetActivityLogs()
        {
            var logs = await activityLogsService.GetActivityLogsAsync();
            return Ok(logs);
        }
    }
}
