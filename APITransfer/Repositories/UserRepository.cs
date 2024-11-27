using APITransfer.Data;
using APITransfer.Interfaces.Repositories;
using APITransfer.Models;
using Microsoft.EntityFrameworkCore;

namespace APITransfer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            Console.WriteLine($"Fetching user by email: {email}"); // Registro del intento
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                Console.WriteLine($"User not found: {email}"); // Registro de error
            }
            else
            {
                Console.WriteLine($"User found: {email}"); // Registro de éxito
            }
            return user;
        }

        public async Task AddUserAsync(User user)
        {
            Console.WriteLine($"Adding user to database: {user.Email}"); // Registro del intento
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            Console.WriteLine($"User added to database: {user.Email}"); // Registro de éxito
        }

    }
}
