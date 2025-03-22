using DAL.Datas;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implements
{
    public class UserRepository : IUserRepository
    {
        private readonly SkinCareContext dbContext;

        public UserRepository()
        {
            dbContext = new();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await dbContext.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
        }

        public async Task<User> AddUserAsync(User user)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetUserByPhoneNumberAsync(string phoneNumber)
        {
            return await dbContext.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
        }
    }
}
