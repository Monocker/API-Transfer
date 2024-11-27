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
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                return NotFound(new ResponseHelper<string>(false, "User not found"));

            return Ok(new ResponseHelper<User>(true, "User retrieved successfully", user));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(new ResponseHelper<IEnumerable<User>>(true, "Users retrieved successfully", users));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseHelper<string>(false, "Invalid data"));

            await _userRepository.AddUserAsync(user);
            return Ok(new ResponseHelper<User>(true, "User created successfully", user));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseHelper<string>(false, "Invalid data"));

            var existingUser = await _userRepository.GetUserByIdAsync(id);
            if (existingUser == null)
                return NotFound(new ResponseHelper<string>(false, "User not found"));

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.Role = user.Role;

            await _userRepository.UpdateUserAsync(existingUser);
            return Ok(new ResponseHelper<User>(true, "User updated successfully", existingUser));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                return NotFound(new ResponseHelper<string>(false, "User not found"));

            await _userRepository.DeleteUserAsync(id);
            return Ok(new ResponseHelper<string>(true, "User deleted successfully"));
        }
    }
}
