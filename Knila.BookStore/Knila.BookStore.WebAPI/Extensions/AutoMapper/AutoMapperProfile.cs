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

            #region WebDomain

            CreateMap<WebDomainViewModel, WebDomain>().ReverseMap();

            CreateMap<Knila.BookStore.WebAPI.Models.Registrar, Knila.BookStore.Domain.Registrar>().ReverseMap();
            CreateMap<Knila.BookStore.WebAPI.Models.Registrant, Knila.BookStore.Domain.Registrant>().ReverseMap();
            CreateMap<Knila.BookStore.WebAPI.Models.Admin, Knila.BookStore.Domain.Admin>().ReverseMap();
            CreateMap<Knila.BookStore.WebAPI.Models.Tech, Knila.BookStore.Domain.Tech>().ReverseMap();
            CreateMap<Knila.BookStore.WebAPI.Models.Billing, Knila.BookStore.Domain.Billing>().ReverseMap();

            #endregion WebDomain

            #region Authentication

            CreateMap<RegisterUserViewModel, User>().ReverseMap();

            CreateMap<LoginUserViewModel, User>().ReverseMap();

            #endregion Authentication
        }
    }
}