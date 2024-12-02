using CapstoneIdeaGenerator.Server.Data.DbContext;
using CapstoneIdeaGenerator.Server.Entities.DTOs;
using CapstoneIdeaGenerator.Server.Entities.Models;
using CapstoneIdeaGenerator.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CapstoneIdeaGenerator.Server.Services
{
    public class ActivityLogsService : IActivityLogsService
    {
        private readonly WebApplicationDbContext dbContext;

        public ActivityLogsService(WebApplicationDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }


        public async Task AddActivityLogs(ActivityLogsDTO request)
        {
            var log = new ActivityLogs
            {
                AdminId = request.AdminId,
                Name = request.Name,
                Action = request.Action,
                Details = request.Details,
            };

            dbContext.ActivityLogs.Add(log);
            await dbContext.SaveChangesAsync();
        }


        public async Task<IEnumerable<ActivityLogsDTO>> GetAllActivityLogs()
        {
            var logs = await dbContext.ActivityLogs
                .Select(al => new ActivityLogsDTO
                {
                    AdminId = al.AdminId,
                    Name = al.Name,
                    Action = al.Action,
                    Details = al.Details,
                    Timestamp = al.Timestamp
                })
                .ToListAsync();

            return logs;
        }
    }
}
