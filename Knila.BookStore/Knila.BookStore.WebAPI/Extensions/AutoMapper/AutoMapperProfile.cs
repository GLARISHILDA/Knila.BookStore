namespace Knila.BookStore.WebAPI.Extensions.AutoMapper
{
    using global::AutoMapper;
    using Knila.BookStore.Domain;
    using Knila.BookStore.WebAPI.Models;
    using System.Net;

    public class AutoMapperProfile : Profile

    {
        // Policy to ViewModel and Vice versa - Data Transfer (Object to Object Mapping)
        public AutoMapperProfile()
        {
            AllowNullDestinationValues = true;

            CreateMap<BookViewModel, Book>().ReverseMap();
            CreateMap<IpAddressViewModel, IpAddress>().ReverseMap();
        }
    }
}