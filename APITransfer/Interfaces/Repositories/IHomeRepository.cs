using APITransfer.DTOs;

namespace APITransfer.Interfaces.Repositories
{
    public interface IHomeRepository
    {
        Task<IEnumerable<BasicInfoDto>> GetAllAgenciesAsync();
        Task<IEnumerable<BasicInfoDto>> GetAllHotelsAsync();
        Task<IEnumerable<BasicInfoDto>> GetAllUnitsAsync();
        Task<IEnumerable<BasicInfoDto>> GetAllZonesAsync();
    }
}
