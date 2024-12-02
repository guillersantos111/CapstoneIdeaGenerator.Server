using CapstoneIdeaGenerator.Server.Entities.AuthenticationModels;

namespace CapstoneIdeaGenerator.Server.Entities.Models
{
    public class ActivityLogs
    {
        public int ActivityLogsId { get; set; }
        public int AdminId { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
        public string Details { get; set; }
        public DateTime? Timestamp { get; set; } = DateTime.UtcNow;

        public Admins Admins { get; set; }
    }
}
