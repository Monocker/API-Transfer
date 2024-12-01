using APITransfer.DTOs;
using APITransfer.Models;

namespace APITransfer.Interfaces.Repositories
{
    public interface IPickupRepository
    {
        Task<IEnumerable<PickupDto>> GetAllPickupsAsync();
        Task<PickupDto> GetPickupByIdAsync(Guid id);
        Task AddPickupAsync(Pickup pickup);
        Task UpdatePickupAsync(Pickup pickup);
        Task DeletePickupAsync(Guid id);
    }
}
