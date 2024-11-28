using APITransfer.Data;
using APITransfer.Interfaces.Repositories;
using APITransfer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APITransfer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtener usuario por ID
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        // Obtener todos los usuarios
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // Agregar usuario
        public async Task AddUserAsync(User user)
        {
            Console.WriteLine($"Adding user to database: {user.Email}"); // Registro del intento
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            Console.WriteLine($"User added to database: {user.Email}"); // Registro de éxito
        }

        // Actualizar usuario
        public async Task UpdateUserAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;
                existingUser.Role = user.Role;

                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();
            }
        }

        // Eliminar usuario
        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        // Obtener usuario por email
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
