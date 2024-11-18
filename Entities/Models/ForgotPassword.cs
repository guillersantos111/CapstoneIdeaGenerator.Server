using System.ComponentModel.DataAnnotations;

namespace CapstoneIdeaGenerator.Server.Entities.AuthenticationModels
{
    public class ForgotPassword
    {
        public string Email { get; set; } = string.Empty;
        public string NewPassword { get; set; }
        public string Token { get; set; }
    }
}
