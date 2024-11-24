using CapstoneIdeaGenerator.Server.Entities.DTOs;
using CapstoneIdeaGenerator.Server.Entities.Models;

namespace CapstoneIdeaGenerator.Server.Services.Interfaces
{
    public interface IActivityLogsService
    {
        Task LogActivity(ActivityLogsDTO activityLogsDTO);
        Task<List<ActivityLogsDTO>> GetActivityLogsAsync();
    }
}
