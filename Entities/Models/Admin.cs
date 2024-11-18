using System.ComponentModel.DataAnnotations;

namespace CapstoneIdeaGenerator.Server.Entities.AuthenticationModels
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Gender { get; set; } = string.Empty;

        [Required]
        public string Age { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;
    }
}
