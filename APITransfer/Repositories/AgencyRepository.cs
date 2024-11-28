using APITransfer.Data;
using APITransfer.Interfaces.Repositories;
using APITransfer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APITransfer.Repositories
{
    public class AgencyRepository : IAgencyRepository
    {
        private readonly ApplicationDbContext _context;

        public AgencyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtener todas las agencias
        public async Task<IEnumerable<Agency>> GetAllAgenciesAsync()
        {
            return await _context.Agencies.ToListAsync();
        }

        // Obtener agencia por ID
        public async Task<Agency> GetAgencyByIdAsync(Guid id)
        {
            return await _context.Agencies.FindAsync(id);
        }

        // Agregar una nueva agencia
        public async Task AddAgencyAsync(Agency agency)
        {
            _context.Agencies.Add(agency);
            await _context.SaveChangesAsync();
        }

        // Actualizar una agencia existente
        public async Task UpdateAgencyAsync(Agency agency)
        {
            _context.Agencies.Update(agency);
            await _context.SaveChangesAsync();
        }

        // Eliminar una agencia
        public async Task DeleteAgencyAsync(Guid id)
        {
            var agency = await _context.Agencies.FindAsync(id);
            if (agency != null)
            {
                _context.Agencies.Remove(agency);
                await _context.SaveChangesAsync();
            }
        }
    }
}
