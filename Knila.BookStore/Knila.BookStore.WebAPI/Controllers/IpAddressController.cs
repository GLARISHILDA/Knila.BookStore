using AutoMapper;
using Knila.BookStore.Domain;
using Knila.BookStore.RepositoryInterface;
using Knila.BookStore.ServiceConcrete;
using Knila.BookStore.ServiceInterface;
using Knila.BookStore.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Knila.BookStore.WebAPI.Controllers
{
    [ApiController]
    [Route("api/ipaddress")]
    public class IpAddressController : ControllerBase
    {
        private readonly ILogger<IpAddressController> _logger;
        private readonly IIpAddressService _ipAddressService;

        private readonly IMapper _mapper;

        public IpAddressController(ILogger<IpAddressController> logger, IIpAddressService ipAddressService, IMapper mapper)
        {
            this._logger = logger;
            this._ipAddressService = ipAddressService;
            this._mapper = mapper;
        }

        [HttpGet]
        [Route("GetIPAddress")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IpAddressViewModel))]
        public async Task<IActionResult> GetIPAddressAsync()
        {
            IpAddress ipAddress = new IpAddress();
            IpAddressViewModel ipAddressViewModel = new IpAddressViewModel();

            ipAddress = await this._ipAddressService.GetIPAddressAsync("");

            ipAddressViewModel = this._mapper.Map<IpAddressViewModel>(ipAddress);

            return this.Ok(ipAddressViewModel);
        }
    }
}