using APITransfer.Models;

namespace APITransfer.Interfaces.Repositories
{
    public interface IZoneRepository
    {
        Task<IEnumerable<Zone>> GetAllZonesAsync();
        Task<Zone> GetZoneByIdAsync(Guid id);
        Task AddZoneAsync(Zone zone);
        Task UpdateZoneAsync(Zone zone);
        Task DeleteZoneAsync(Guid id);
    }
}
