using APITransfer.Data;
using APITransfer.DTOs;
using APITransfer.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace APITransfer.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _context;

        public HomeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BasicInfoDto>> GetAllAgenciesAsync()
        {
            return await _context.Agencies
                .Select(a => new BasicInfoDto { Id = a.Id, Name = a.Name })
                .ToListAsync();
        }

        public async Task<IEnumerable<BasicInfoDto>> GetAllHotelsAsync()
        {
            return await _context.Hotels
                .Select(h => new BasicInfoDto { Id = h.Id, Name = h.Name })
                .ToListAsync();
        }

        public async Task<IEnumerable<BasicInfoDto>> GetAllUnitsAsync()
        {
            return await _context.Units
                .Select(u => new BasicInfoDto { Id = u.Id, Name = u.Name })
                .ToListAsync();
        }

        public async Task<IEnumerable<BasicInfoDto>> GetAllZonesAsync()
        {
            return await _context.Zones
                .Select(z => new BasicInfoDto { Id = z.Id, Name = z.Name })
                .ToListAsync();
        }
    }
}
