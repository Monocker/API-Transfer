using APITransfer.Data;
using APITransfer.Interfaces.Repositories;
using APITransfer.Models;
using Microsoft.EntityFrameworkCore;

namespace APITransfer.Repositories
{
    public class ZoneRepository : IZoneRepository
    {
        private readonly ApplicationDbContext _context;

        public ZoneRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Zone>> GetAllZonesAsync()
        {
            return await _context.Zones.ToListAsync();
        }

        public async Task<Zone> GetZoneByIdAsync(Guid id)
        {
            return await _context.Zones.FindAsync(id);
        }

        public async Task AddZoneAsync(Zone zone)
        {
            _context.Zones.Add(zone);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateZoneAsync(Zone zone)
        {
            _context.Zones.Update(zone);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteZoneAsync(Guid id)
        {
            var zone = await _context.Zones.FindAsync(id);
            if (zone != null)
            {
                _context.Zones.Remove(zone);
                await _context.SaveChangesAsync();
            }
        }
    }
}
