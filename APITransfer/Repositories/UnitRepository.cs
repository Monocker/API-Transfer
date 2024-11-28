using APITransfer.Data;
using APITransfer.Interfaces.Repositories;
using APITransfer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APITransfer.Repositories
{
    public class UnitRepository : IUnitRepository
    {
        private readonly ApplicationDbContext _context;

        public UnitRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Unit>> GetAllUnitsAsync()
        {
            return await _context.Units.Include(u => u.Agency).ToListAsync();
        }

        public async Task<Unit> GetUnitByIdAsync(Guid id)
        {
            return await _context.Units.Include(u => u.Agency).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task AddUnitAsync(Unit unit)
        {
            _context.Units.Add(unit);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUnitAsync(Unit unit)
        {
            _context.Units.Update(unit);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUnitAsync(Guid id)
        {
            var unit = await _context.Units.FindAsync(id);
            if (unit != null)
            {
                _context.Units.Remove(unit);
                await _context.SaveChangesAsync();
            }
        }
    }
}
