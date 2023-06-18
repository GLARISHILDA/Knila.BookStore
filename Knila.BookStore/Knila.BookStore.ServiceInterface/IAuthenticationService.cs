using Knila.BookStore.Domain.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knila.BookStore.ServiceInterface
{
    public interface IAuthenticationService
    {
        Task<List<User>> GetAllUsersAsync();

        Task<long> RegisterUserAsync(User user);

        Task<User> GetUserDetailsAsync(string emailAddress);

        Task<UserAuthentication> ValidateLoginUserAsync(User user);
    }
}