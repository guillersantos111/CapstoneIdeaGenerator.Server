using Microsoft.AspNetCore.Mvc;
using CapstoneIdeaGenerator.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using CapstoneIdeaGenerator.Server.Entities.DTOs;

namespace CapstoneIdeaGenerator.Server.Controllers
{
    namespace CapstoneIdeaGenerator.Server.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class AuthenticationController : ControllerBase
        {
            private readonly IAuthenticationService authenticationService;

            public AuthenticationController(IAuthenticationService authenticationService)
            {
                this.authenticationService = authenticationService;
            }


            [HttpGet, Authorize(Roles = "Admin")]
            public async Task<ActionResult<string>> GetMe()
            {
                var userName = await authenticationService.GetMyName();
                return Ok(userName);
            }


            [HttpGet("accounts"), Authorize(Roles = "Admin")]
            public async Task<ActionResult<IEnumerable<AdminDTO>>> GetAllAccounts()
            {
                try
                {
                    var accounts = await authenticationService.GetAllAccounts();
                    return Ok(accounts);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal Server Error: {ex.Message}");
                }
            }


            [HttpPost("register"), Authorize(Roles = "Admin")]
            public async Task<ActionResult<AdminDTO>> Register(AdminRegisterDTO request)
            {
                try
                {
                    var admin = await authenticationService.RegisterAdmin(request);
                    return Ok(admin);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { ex.Message });
                }
            }


            [HttpPost("login")]
            public async Task<ActionResult<string>> Login([FromBody]AdminLoginDTO request)
            {
                try
                {
                    var token = await authenticationService.LoginAdmin(request);
                    return Ok(token);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { ex.Message});
                }
            }


            [HttpPost("forgot-password")]
            public async Task<IActionResult> ForgotPassword([FromBody] AdminForgotPasswordDTO request)
            {
                try
                {
                    var token = await authenticationService.GeneratePasswordResetToken(request);

                    return Ok( new 
                    { 
                        Message = "Password Reset Token Generated", 
                        Token = token 
                    });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { ex.Message });
                }
            }


            [HttpPost("reset-password")]
            public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO request)
            {
                try
                {
                    await authenticationService.ResetPassword(request.Token, request.NewPassword);
                    return Ok(new { Message = "Password Has Been Reset Successfully" });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { ex.Message });
                }
            }
        }
    }
}
