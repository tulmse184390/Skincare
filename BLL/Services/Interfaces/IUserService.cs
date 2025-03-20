using DAL.Entities;

namespace BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> Login(string email, string password);
        Task<User?> Register(User user);
    }
}
