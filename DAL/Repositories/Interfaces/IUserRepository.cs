using DAL.Entities;

namespace DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User> AddUserAsync(User user);
    }
}
