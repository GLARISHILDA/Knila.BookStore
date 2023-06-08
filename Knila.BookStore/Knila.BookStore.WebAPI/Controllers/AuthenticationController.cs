using AutoMapper;
using Knila.BookStore.Domain.Authentication;
using Knila.BookStore.ServiceInterface;
using Knila.BookStore.WebAPI.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Knila.BookStore.WebAPI.Controllers
{
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

        [AllowAnonymous]
        [HttpGet("RegisterUser")]
        public async Task<IActionResult> GetRegisterUserAsync()
        {
            return View("RegisterUser");
        }

        [AllowAnonymous]
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUserAsync(RegisterUserViewModel registerUserViewModel)
        {
            User user = new User();
            user = this._mapper.Map<User>(registerUserViewModel);
            long userId = await this._authenticationService.RegisterUserAsync(user);
            return this.Json("User has been Created");
        }

        [AllowAnonymous]
        [HttpGet("LoginUser")]
        public async Task<IActionResult> GetLoginUserAsync()
        {
            return View("LoginUser");
        }

        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUserAsync(LoginUserViewModel loginUserViewModel)
        {
            User user = new User();
            user = this._mapper.Map<User>(loginUserViewModel);
            UserAuthentication userAuthentication = new UserAuthentication();
            userAuthentication = await this._authenticationService.ValidateLoginUserAsync(user);
            if (userAuthentication.IsAuthentication == false)
            {
                return this.Json("User Not Found");
            }
            else
            {
                return this.Json("User has been Validated");
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}