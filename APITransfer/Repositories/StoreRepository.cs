using APITransfer.Data;
using APITransfer.DTOs;
using APITransfer.Interfaces.Repositories;
using APITransfer.Models;
using Microsoft.EntityFrameworkCore;

namespace APITransfer.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly ApplicationDbContext _context;

        public StoreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StoreDto>> GetAllStoresAsync()
        {
            return await _context.Stores
                .Select(s => new StoreDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    ZoneId = s.ZoneId
                })
                .ToListAsync();
        }

        public async Task<StoreDto> GetStoreByIdAsync(Guid id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store == null) return null;

            return new StoreDto
            {
                Id = store.Id,
                Name = store.Name,
                ZoneId = store.ZoneId
            };
        }

        public async Task AddStoreAsync(Store store)
        {
            _context.Stores.Add(store);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStoreAsync(Store store)
        {
            _context.Stores.Update(store);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStoreAsync(Guid id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store != null)
            {
                _context.Stores.Remove(store);
                await _context.SaveChangesAsync();
            }
        }
    }
}
