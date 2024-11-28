namespace CapstoneIdeaGenerator.Server.Entities.DTOs
{
    public class ResetPasswordDTO
    {
        public string NewPassword { get; set; }
        public string? Token { get; set; }
    }
}
