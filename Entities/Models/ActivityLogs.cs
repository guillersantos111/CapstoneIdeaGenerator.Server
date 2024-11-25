using CapstoneIdeaGenerator.Server.Entities.AuthenticationModels;
using Swashbuckle.AspNetCore.Annotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CapstoneIdeaGenerator.Server.Entities.Models
{
    public class ActivityLogs
    {
        public Guid ActivityLogId { get; set; }
        public int AdminId { get; set; }
        public string Name { get; set; }
        public string Actions { get; set; }
        public DateTime Timestamp { get; set; }

        public Admin Admin { get; set; }
    }
}
