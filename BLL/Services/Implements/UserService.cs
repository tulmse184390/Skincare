using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Implements;
using DAL.Repositories.Interfaces;

namespace BLL.Services.Implements
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;

        public UserService()
        {
            userRepository = new UserRepository();
        }

        public async Task<User?> Login(string email, string password)
        {
            var user = await userRepository.GetUserByEmailAsync(email);
            if (user == null || user.Password != password)
            {
                return null;
            }
            return user;
        }

        public async Task<User?> Register(User user)
        {
            var tmpUser = await userRepository.GetUserByEmailAsync(user.Email);
            if (tmpUser != null)
            {
                return null;
            }
            return await userRepository.AddUserAsync(user);
        }
    }
}
