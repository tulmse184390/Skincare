using DAL.Entities;

namespace BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> LoginAsync(string email, string password);
        Task<User?> RegisterAsync(User user);
        Task<User?> UpdateProfileAsync(User updateUser);
        Task<User?> ChangePasswordAsync(string userEmail, string password);
    }
}
