using Knila.BookStore.Domain;

namespace Knila.BookStore.ServiceInterface
{
    public interface IWebDomainService
    {
        Task<WebDomain> GetDomainAddressAsync(string domainAddress);
    }
}