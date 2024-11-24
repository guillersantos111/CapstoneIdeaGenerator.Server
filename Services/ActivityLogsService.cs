using CapstoneIdeaGenerator.Server.Data.DbContext;
using CapstoneIdeaGenerator.Server.Entities.DTOs;
using CapstoneIdeaGenerator.Server.Entities.Models;
using CapstoneIdeaGenerator.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
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

        public async Task LogActivity([FromBody] ActivityLogsDTO activityLogsDTO)
        {
            var admin = await dbContext.Admins.FindAsync(activityLogsDTO.AdminId);
            if (admin == null)
            {
                throw new Exception("Admin not found");
            }

            var activityLog = new ActivityLogs
            {
                AdminId = activityLogsDTO.AdminId,
                Name = activityLogsDTO.Name,
                Actions = activityLogsDTO.Actions,
                Timestamp = activityLogsDTO.Timestamp,
                Admin = admin
            };

            dbContext.ActivityLogs.Add(activityLog);
            await dbContext.SaveChangesAsync();
        }

        // Get all activity logs
        public async Task<List<ActivityLogsDTO>> GetActivityLogsAsync()
        {
            return await dbContext.ActivityLogs
                .Include(log => log.Admin)
                .OrderByDescending(log => log.Timestamp)
                .Select(log => new ActivityLogsDTO
                {
                    Name = log.Name,
                    Actions = log.Actions,
                    Timestamp = log.Timestamp
                })
                .ToListAsync();
        }
    }
}
