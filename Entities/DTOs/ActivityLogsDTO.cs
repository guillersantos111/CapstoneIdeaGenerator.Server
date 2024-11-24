namespace CapstoneIdeaGenerator.Server.Entities.DTOs
{
    public class ActivityLogsDTO
    {
        public Guid ActivityLogId { get; set; }
        public int AdminId { get; set; }
        public string Name { get; set; }
        public string Actions { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
