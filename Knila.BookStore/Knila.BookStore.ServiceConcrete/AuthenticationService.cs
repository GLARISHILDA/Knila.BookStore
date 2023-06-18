using Knila.BookStore.Domain.Authentication;
using Knila.BookStore.Infrastructure.Extentsion;
using Knila.BookStore.RepositoryInterface;
using Knila.BookStore.ServiceInterface;
using System.Net.Mail;
using System.Xml.Linq;

namespace Knila.BookStore.ServiceConcrete
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authenticationRepository;

        public AuthenticationService(IAuthenticationRepository authenticationRepository)
        {
            this._authenticationRepository = authenticationRepository;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await this._authenticationRepository.GetAllUsersAsync();
        }

        public async Task<long> RegisterUserAsync(User user)
        {
            user.PasswordSalt = StringExtensions.CreateSalt(15);

            user.PasswordHash = StringExtensions.ComputeSha256Hash(user.Password + user.PasswordSalt);

            return await this._authenticationRepository.RegisterUserAsync(user);
        }

        public async Task<User> GetUserDetailsAsync(string emailAddress)
        {
            return await this._authenticationRepository.GetUserDetailsAsync(emailAddress);
        }

        public async Task<UserAuthentication> ValidateLoginUserAsync(User user)
        {
            UserAuthentication userAuthentication = new UserAuthentication();
            User validateUser = new User();
            validateUser = await this._authenticationRepository.GetUserDetailsAsync(user.EmailAddress);

            if (validateUser == null)
            {
                userAuthentication.IsAuthentication = false;
                return userAuthentication;
            }

            string passwordHash = StringExtensions.ComputeSha256Hash(user.Password + validateUser.PasswordSalt);
            if (passwordHash != validateUser.PasswordHash)
            {
                userAuthentication.IsAuthentication = false;
                return userAuthentication;
            }
            userAuthentication.IsAuthentication = true;
            userAuthentication.UserId = validateUser.UserId;
            userAuthentication.Name = validateUser.Name;
            userAuthentication.EmailAddress = validateUser.EmailAddress;
            userAuthentication.AuthVersion = validateUser.AuthVersion;
            userAuthentication.LoggedOn = DateTime.Now;

            return userAuthentication;
        }
    }
}