using APITransfer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APITransfer.Interfaces.Repositories
{
    public interface IUnitRepository
    {
        Task<IEnumerable<Unit>> GetAllUnitsAsync();
        Task<Unit> GetUnitByIdAsync(Guid id);
        Task AddUnitAsync(Unit unit);
        Task UpdateUnitAsync(Unit unit);
        Task DeleteUnitAsync(Guid id);
    }
}
