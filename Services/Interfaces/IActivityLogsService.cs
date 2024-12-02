using CapstoneIdeaGenerator.Server.Entities.DTOs;
using CapstoneIdeaGenerator.Server.Entities.Models;

namespace CapstoneIdeaGenerator.Server.Services.Interfaces
{
    public interface IActivityLogsService
    {
        Task AddActivityLogs(ActivityLogsDTO request);
        Task<IEnumerable<ActivityLogsDTO>> GetAllActivityLogs();
    }
}
