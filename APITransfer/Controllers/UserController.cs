using APITransfer.DTOs;
using APITransfer.Helpers;
using APITransfer.Interfaces.Repositories;
using APITransfer.Models;
using Microsoft.AspNetCore.Mvc;

namespace APITransfer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                return NotFound(new ResponseHelper<string>(false, "Usuario no encontrado"));

            return Ok(new ResponseHelper<User>(true, "Usuario recuperado con éxito", user));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(new ResponseHelper<IEnumerable<User>>(true, "Usuarios recuperados con éxito", users));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseHelper<string>(false, "Datos inválidos"));

            var user = new User
            {
                Id = Guid.NewGuid(), 
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password, // Aquí deberías aplicar el hash de la contraseña
                RoleId = userDto.RoleId
            };

            await _userRepository.AddUserAsync(user);

            return Ok(new ResponseHelper<User>(true, "Usuario creado con éxito", user));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] User user)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(id);
            if (existingUser == null)
                return NotFound(new ResponseHelper<string>(false, "Usuario no encontrado"));

            user.Id = id;
            await _userRepository.UpdateUserAsync(user);
            return Ok(new ResponseHelper<User>(true, "Usuario actualizado con éxito", user));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                return NotFound(new ResponseHelper<string>(false, "Usuario no encontrado"));

            await _userRepository.DeleteUserAsync(id);
            return Ok(new ResponseHelper<string>(true, "Usuario eliminado con éxito"));
        }
    }
}
