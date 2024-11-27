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
            if (string.IsNullOrEmpty(authDto.Email) || string.IsNullOrEmpty(authDto.Password))
            {
                Console.WriteLine("Login failed: Missing email or password"); // Registro de validación
                return BadRequest(new { Message = "Email and Password are required." });
            }

            try
            {
                Console.WriteLine($"Login attempt for email: {authDto.Email}"); // Registro del intento
                var token = await _authService.Authenticate(authDto.Email, authDto.Password);
                Console.WriteLine($"Token generated for email: {authDto.Email}"); // Registro de éxito
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Unauthorized access for email: {authDto.Email}, Error: {ex.Message}"); // Registro de error
                return Unauthorized(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during login: {ex.Message}"); // Registro de error genérico
                return StatusCode(500, new { Message = "An error occurred while processing your request." });
            }
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                Console.WriteLine("Registration failed: Missing email or password"); // Registro de validación
                return BadRequest(new { Message = "Email and Password are required." });
            }

            try
            {
                Console.WriteLine($"Registration attempt for email: {user.Email}"); // Registro del intento
                await _authService.Register(user);
                Console.WriteLine($"User registered: {user.Email}"); // Registro de éxito
                return Ok(new { Message = "User registered successfully." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during registration: {ex.Message}"); // Registro de error
                return StatusCode(500, new { Message = "An error occurred while registering the user." });
            }
        }

    }
}
