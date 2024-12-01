using APITransfer.Data;
using APITransfer.DTOs;
using APITransfer.Interfaces.Repositories;
using APITransfer.Models;
using Microsoft.EntityFrameworkCore;

namespace APITransfer.Repositories
{
    public class PickupRepository : IPickupRepository
    {
        private readonly ApplicationDbContext _context;

        public PickupRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PickupDto>> GetAllPickupsAsync()
        {
            return await _context.Pickups
                .Select(p => new PickupDto
                {
                    Id = p.Id,
                    PickupTime = p.PickupTime.ToString(@"hh\:mm"), // Formato HH:mm
                    HotelId = p.HotelId
                })
                .ToListAsync();
        }

        public async Task<PickupDto> GetPickupByIdAsync(Guid id)
        {
            var pickup = await _context.Pickups.FindAsync(id);
            if (pickup == null) return null;

            return new PickupDto
            {
                Id = pickup.Id,
                PickupTime = pickup.PickupTime.ToString(@"hh\:mm"), // Formato HH:mm
                HotelId = pickup.HotelId
            };
        }

        public async Task AddPickupAsync(Pickup pickup)
        {
            _context.Pickups.Add(pickup);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePickupAsync(Pickup pickup)
        {
            _context.Pickups.Update(pickup);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePickupAsync(Guid id)
        {
            var pickup = await _context.Pickups.FindAsync(id);
            if (pickup != null)
            {
                _context.Pickups.Remove(pickup);
                await _context.SaveChangesAsync();
            }
        }
    }
}
