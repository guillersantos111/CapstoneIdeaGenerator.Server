using CapstoneIdeaGenerator.Server.Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace CapstoneIdeaGenerator.Server.Entities.AuthenticationModels
{
    public class Admins
    {
        [Key]
        public int AdminId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public string Age { get; set; } = string.Empty;

        public DateTime DateJoined { get; set; }

        public string Email { get; set; } = string.Empty;

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string RefreshToken { get; set; } = string.Empty;

        public DateTime TokenCreated { get; set; }

        public DateTime TokenExpires { get; set; }
    }
}
