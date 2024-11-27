using APITransfer.Models;

namespace APITransfer.Interfaces.Services
{
    public interface IAuthService
    {
        Task<string> Authenticate(string email, string password);
        Task Register(User user);
    }
}
