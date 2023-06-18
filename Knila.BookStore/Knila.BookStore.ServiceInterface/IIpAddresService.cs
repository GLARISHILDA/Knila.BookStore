using Knila.BookStore.Domain;

namespace Knila.BookStore.ServiceInterface
{
    public interface IIpAddressService
    {
        Task<IpAddress> GetIPAddressAsync(string ipAddress);
    }
}