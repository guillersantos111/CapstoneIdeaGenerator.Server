using CapstoneIdeaGenerator.Server.Data.DbContext;
using CapstoneIdeaGenerator.Server.Entities.AuthenticationModels;
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
        private Admins admin;

        public ActivityLogsService(WebApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task LogActivity([FromBody] ActivityLogsDTO activityLogsDTO)
        {
            var adminExists = await dbContext.Admins.AnyAsync(a => a.AdminId == activityLogsDTO.AdminId);
            if (!adminExists)
            {
                throw new Exception("Admin not found");
            }

            var activityLog = new ActivityLogs
            {
                AdminId = activityLogsDTO.AdminId,
                Name = admin.Name,
                Actions = activityLogsDTO.Actions,
                Timestamp = DateTime.UtcNow
            };

            dbContext.ActivityLogs.Add(activityLog);
            await dbContext.SaveChangesAsync();
        }


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
