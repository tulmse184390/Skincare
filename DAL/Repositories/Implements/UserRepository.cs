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

        public async Task<User?> UpdateUserAsync(User updateUser)
        {
            var tmpUser = await dbContext.Users.FirstOrDefaultAsync(x => x.UserId == updateUser.UserId);
            if (tmpUser == null)
            {
                return null;
            }
            tmpUser.FirstName = updateUser.FirstName;
            tmpUser.LastName = updateUser.LastName;
            tmpUser.Password = updateUser.Password;
            tmpUser.Gender = updateUser.Gender;
            tmpUser.PhoneNumber = updateUser.PhoneNumber;

            await dbContext.SaveChangesAsync();
            return tmpUser;
        }
    }
}
