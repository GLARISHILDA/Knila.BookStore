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
    [Route("api/webdomain")]
    public class WebDomainController : ControllerBase
    {
        private readonly ILogger<WebDomainController> _logger;
        private readonly IWebDomainService _domainService;
        private readonly IMapper _mapper;

        public WebDomainController(ILogger<WebDomainController> logger, IWebDomainService domainService, IMapper mapper)
        {
            this._logger = logger;
            this._domainService = domainService;
            this._mapper = mapper;
        }

        [HttpGet]
        [Route("GetDomainAddress")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WebDomainViewModel))]
        public async Task<IActionResult> GetDomainAddressAsync(string domainaddress)
        {
            WebDomain domainAddress = new WebDomain();
            WebDomainViewModel DomainViewModel = new WebDomainViewModel();

            domainAddress = await this._domainService.GetDomainAddressAsync(domainaddress);

            DomainViewModel = this._mapper.Map<WebDomainViewModel>(domainAddress);

            return this.Ok(DomainViewModel);
        }
    }
}