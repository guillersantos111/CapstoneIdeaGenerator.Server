using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneIdeaGenerator.Server.Entities.DTOs
{
    public class AdminRegisterDTO
    {
        public int AdminId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public int Age { get; set; }

        public DateTime DateJoined { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
