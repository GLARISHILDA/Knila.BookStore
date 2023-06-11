using AutoMapper;
using Knila.BookStore.Domain.Authentication;
using Knila.BookStore.ServiceInterface;
using Knila.BookStore.WebAPI.Models;
using Knila.BookStore.WebAPI.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Knila.BookStore.WebAPI.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : Controller
    {
        private readonly ILogger<AuthenticationController> _logger;

        private readonly IAuthenticationService _authenticationService;

        private readonly IMapper _mapper;

        public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationService authenticationService, IMapper mapper)
        {
            this._logger = logger;
            this._authenticationService = authenticationService;
            this._mapper = mapper;
        }

        [HttpGet]
        [Route("RegisterUser1")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<IActionResult> RegisterUser1()
        {
            return this.Ok("API is up");
        }

        [HttpPost]
        [Route("RegisterUser")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(RegisterUserViewModel))]
        public async Task<IActionResult> RegisterUserAsync(RegisterUserViewModel registerUserViewModel)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            User user = new User();
            user = this._mapper.Map<User>(registerUserViewModel);
            long userId = await this._authenticationService.RegisterUserAsync(user);
            return this.Ok("User has been Created");
        }

        [HttpPost]
        [Route("LoginUser")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(LoginUserViewModel))]
        public async Task<IActionResult> LoginUserAsync(LoginUserViewModel loginUserViewModel)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            User user = new User();
            user = this._mapper.Map<User>(loginUserViewModel);
            UserAuthentication userAuthentication = new UserAuthentication();
            userAuthentication = await this._authenticationService.ValidateLoginUserAsync(user);
            if (userAuthentication.IsAuthentication == false)
            {
                return this.NotFound("User Not Found");
            }
            else
            {
                int authTokenExpireInMinutes = 10;

                var expire = DateTime.Now.AddMinutes(authTokenExpireInMinutes);

                var authClaims = new List<Claim>
                {
                    //new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    //new Claim(ClaimTypes.Name, user.Name),
                    //new Claim(ClaimTypes.Email, user.EmailAddress),
                    new Claim(ClaimTypes.Expiration, expire.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                //List<UserRole> roles = await this._userAuthenticationRepository.GetUserRolesAsync(user.UserId);

                //foreach (var item in roles)
                //{
                //    authClaims.Add(new Claim(ClaimTypes.Role, item.RoleName));
                //}

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("eyJhbGciOiJIUzUxMiJ9.eyJSb2xlIjoiQWRtaW4iLCJJc3N1ZXIiOiJJc3N1ZXIiLCJVc2VybmFtZSI6IkphdmFJblVzZSIsImV4cCI6MTYzNjI4MDE5NCwiaWF0IjoxNjM2MjgwMTk0fQ.1Kv_-41VhLaf5QDkfu2tMgsUIGV_lZsWfaZlmSap55CSAz3fGHQ9Qzf0PmxAaUfractckzOS5bjoB9EaxRLVbQ"));

                var token = new JwtSecurityToken(
                    issuer: "https://localhost:7282/",
                    audience: "https://localhost:7282/",
                    expires: expire,
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

                string authToken = new JwtSecurityTokenHandler().WriteToken(token);

                return this.Ok(authToken);
            }
        }
    }
}