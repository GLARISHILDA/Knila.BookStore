using FizzWare.NBuilder;
using Knila.BookStore.Domain;
using Knila.BookStore.Infrastructure.DbConnection;
using Knila.BookStore.RepositoryInterface;
using Knila.BookStore.ServiceInterface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace Knila.BookStore.ServiceConcrete
{
    public class WebDomainService : IWebDomainService
    {
        private readonly IConfiguration _configuration;

        public WebDomainService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task<WebDomain> GetDomainAddressAsync(string domainAddress)
        {
            WebDomain domain1 = new WebDomain();

            //        "DomainService": {
            //            "APIUrl": "https://api.ip2whois.com/v2",
            //"APIKey": "28F16177A050B488A3928455D729C614"
            //        }

            // https://api.ip2whois.com/v2?key=28F16177A050B488A3928455D729C614&domain=example.com

            string api = this._configuration["DomainService:APIUrl"];

            var client = new RestClient(this._configuration["DomainService:APIUrl"]);
            var request = new RestRequest($"?key={this._configuration["DomainService:APIKey"]}&domain={domainAddress}");
            var response = await client.ExecuteGetAsync(request);

            domain1 = JsonConvert.DeserializeObject<WebDomain>(response.Content);
            return domain1;
        }
    }
}