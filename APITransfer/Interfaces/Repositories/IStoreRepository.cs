using APITransfer.DTOs;
using APITransfer.Models;

namespace APITransfer.Interfaces.Repositories
{
    public interface IStoreRepository
    {
        Task<IEnumerable<StoreDto>> GetAllStoresAsync();
        Task<StoreDto> GetStoreByIdAsync(Guid id);
        Task AddStoreAsync(Store store);
        Task UpdateStoreAsync(Store store);
        Task DeleteStoreAsync(Guid id);
    }
}
