using CapstoneIdeaGenerator.Server.Services;
using CapstoneIdeaGenerator.Server.Entities.AuthenticationModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CapstoneIdeaGenerator.Server.Services.Interfaces;
using CapstoneIdeaGenerator.Server.Data.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.Data;

namespace CapstoneIdeaGenerator.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }


        [HttpPost("register")]
        public IActionResult Register([FromBody] Admin admin)
        {
            bool result = _authenticationService.Register(admin);

            if (result)
            {
                return Ok("Registration successful.");
            }

            return BadRequest("Admin already exists.");
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthRequest request)
        {
            string token = _authenticationService.Login(request);

            if (token == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            return Ok(new {Message = "Login Successfully", Token = token });
        }


        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword([FromBody] string email)
        {
            string resetToken = _authenticationService.ForgotPassword(email);

            if (resetToken != null)
            {
                return Ok(new {Message = "Password reset token has been generated", Token =  resetToken});
            }

            return NotFound("Admin not found.");
        }


        [HttpPost("reset-password")]
        public IActionResult ResetPassword([FromBody] ForgotPassword request)
        {
            bool result = _authenticationService.ResetPassword(request.Email, request.NewPassword, request.Token);

            if (result)
            {
                return Ok("Password has been reset successfully.");
            }

            return BadRequest("Invalid token or admin. Ensure that the email and token match the database records.");
        }


        [HttpGet("accounts")]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAllAccounts()
        {
            try
            {
                var accounts = await _authenticationService.GetAllAccounts();

                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAccount(int id)
        {
            try
            {
                var account = await _authenticationService.RemoveAccount(id);
                await _authenticationService.RemoveAccount(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
