using DAL.Entities;

namespace DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User> AddUserAsync(User user);
        Task<User?> GetUserByPhoneNumberAsync(string phoneNumber);
        Task<User?> UpdateUserAsync(User updateUser);
    }
}
