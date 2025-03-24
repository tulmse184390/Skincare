using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Implements;
using DAL.Repositories.Interfaces;
using System.Text.RegularExpressions;

namespace BLL.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService()
        {
            userRepository = new UserRepository();
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            var user = await userRepository.GetUserByEmailAsync(email);
            if (user == null || user.Password != password || user.Status == "Inactive")
            {
                return null;
            }
            return user;
        }

        public async Task<User?> RegisterAsync(User user)
        {
            var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            var phoneNumberPattern = @"^0\d{9}$";
            if (!Regex.IsMatch(user.Email, emailPattern))
            {
                throw new Exception("This email is invalid!");
            }
            if (user.PhoneNumber != null && !Regex.IsMatch(user.PhoneNumber, phoneNumberPattern))
            {
                throw new Exception("This phone number is invalid!");
            }
            var tmpUser = await userRepository.GetUserByEmailAsync(user.Email);
            if (tmpUser != null)
            {
                throw new Exception("This email has been taken!");
            }
            if (user.PhoneNumber != null)
            {
                tmpUser = await userRepository.GetUserByPhoneNumberAsync(user.PhoneNumber);
                if (tmpUser != null)
                {
                    throw new Exception("This phone number has been taken!");
                }
            }

            return await userRepository.AddUserAsync(user);
        }

        public async Task<User?> UpdateProfileAsync(User updateUser)
        {
            return await userRepository.UpdateUserAsync(updateUser);
        }

        public async Task<User?> ChangePasswordAsync(string userEmail, string password)
        {
            var user = await userRepository.GetUserByEmailAsync(userEmail);
            if (user != null)
            {
                user.Password = password;
                return await userRepository.UpdateUserAsync(user);
            }
            return null;
        }
    }
}
