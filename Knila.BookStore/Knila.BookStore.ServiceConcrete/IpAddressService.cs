using FizzWare.NBuilder;
using Knila.BookStore.Domain;
using Knila.BookStore.Infrastructure.DbConnection;
using Knila.BookStore.RepositoryInterface;
using Knila.BookStore.ServiceInterface;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace Knila.BookStore.ServiceConcrete
{
    public class IpAddressService : IIpAddressService
    {
        public IpAddressService()
        {
        }

        public async Task<IpAddress> GetIPAddressAsync(string ipAddress)
        {
            IpAddress ipAddress1 = new IpAddress();
            var client = new RestClient("https://api.ip2location.io/");
            var request = new RestRequest($"?key=28F16177A050B488A3928455D729C614&ip={ipAddress}");
            var response = await client.ExecuteGetAsync(request);

            ipAddress1 = JsonConvert.DeserializeObject<IpAddress>(response.Content);
            return ipAddress1;
        }
    }
}