using APITransfer.DTOs;
using APITransfer.Interfaces.Services;
using APITransfer.Models;
using Microsoft.AspNetCore.Mvc;

namespace APITransfer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthDto authDto)
        {
            try
            {
                var token = await _authService.Authenticate(authDto.Email, authDto.Password);
                return Ok(new { Token = token });
            }
            catch
            {
                return Unauthorized();
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            await _authService.Register(user);
            return Ok();
        }
    }
}
