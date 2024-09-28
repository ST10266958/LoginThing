using AzLogin.Models;

namespace AzLogin.Services
{
    public interface IAuthServicecs
    {
        Task<bool> RegisterAsync(string email, string password, string fullName);
        Task<User> LoginAsync(string email, string password);
    }
}
