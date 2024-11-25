namespace CapstoneIdeaGenerator.Server.Entities.AuthenticationModels
{
    public class AdminDTO
    {
        public string Name { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public string Age { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateTime DateJoined { get; set; }

        public string Password { get; set; } = string.Empty;
    }
}
