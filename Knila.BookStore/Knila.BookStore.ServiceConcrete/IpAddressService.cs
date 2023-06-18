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
    public class IpAddressService : IIpAddressService
    {
        private readonly IConfiguration _configuration;

        public IpAddressService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task<IpAddress> GetIPAddressAsync(string ipAddress)
        {
            IpAddress ipAddress1 = new IpAddress();

            string api = this._configuration["IpAddressService:APIAddress"];

            var client = new RestClient(this._configuration["IpAddressService:APIAddress"]);
            var request = new RestRequest($"?key={this._configuration["IpAddressService:APIKey"]}&ip={ipAddress}");
            var response = await client.ExecuteGetAsync(request);

            ipAddress1 = JsonConvert.DeserializeObject<IpAddress>(response.Content);
            return ipAddress1;
        }
    }
}