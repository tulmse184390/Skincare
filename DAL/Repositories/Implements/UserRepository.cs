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
            return await dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User> AddUserAsync(User user)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return user;
        }
    }
}
