namespace Knila.BookStore.WebAPI.Extensions.AutoMapper
{
    using global::AutoMapper;
    using Knila.BookStore.Domain;
    using Knila.BookStore.Domain.Authentication;
    using Knila.BookStore.WebAPI.Models;
    using Knila.BookStore.WebAPI.Models.Authentication;

    public class AutoMapperProfile : Profile

    {
        // Policy to ViewModel and Vice versa - Data Transfer (Object to Object Mapping)
        public AutoMapperProfile()
        {
            AllowNullDestinationValues = true;

            CreateMap<BookViewModel, Book>().ReverseMap();

            CreateMap<IpAddressViewModel, IpAddress>().ReverseMap();


            #region Authentication

            CreateMap<RegisterUserViewModel, User>().ReverseMap();

            CreateMap<LoginUserViewModel, User>().ReverseMap();

            #endregion Authentication
        }
    }
}