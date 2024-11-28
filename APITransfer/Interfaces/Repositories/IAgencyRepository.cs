using APITransfer.Models;

namespace APITransfer.Interfaces.Repositories
{
    public interface IAgencyRepository
    {
        Task<IEnumerable<Agency>> GetAllAgenciesAsync();
        Task<Agency> GetAgencyByIdAsync(Guid id);
        Task AddAgencyAsync(Agency agency);
        Task UpdateAgencyAsync(Agency agency);
        Task DeleteAgencyAsync(Guid id);
    }
}

