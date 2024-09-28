using AzLogin.Repository;
using BCrypt.Net;

namespace AzLogin.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepoistory)
        {
            _userRepository = userRepoistory;
        }

        public async Task<bool> RegisterAsync(string email, string password, string fullName)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(email);
            if (existingUser != null)
                return false;

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                Rowkey = email,
                email = email,
                FullName = fullName,
                PasswordHash = passwordHash
            };
            return await _userRepository.CreateUserAsync(user);

        }
        public async Task<User> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user != null)
                return null;

            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            return isValid ? user : null;
        }
    }
}
    

