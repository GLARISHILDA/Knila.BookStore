using FizzWare.NBuilder;
using Knila.BookStore.Domain;
using Knila.BookStore.Infrastructure.DbConnection;
using Knila.BookStore.RepositoryInterface;
using Knila.BookStore.ServiceInterface;

namespace Knila.BookStore.ServiceConcrete
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            this._bookRepository = bookRepository;
        }

        public async Task<List<Book>> GetAllBookDetailsSort1Async()
        {
            List<Book> books = new List<Book>();
            books = await this._bookRepository.GetAllBookDetailsSort1Async();

            books = books
                        .OrderBy(x => x.Publisher)
                        .ThenBy(x => x.AuthorLastName)
                        .ThenBy(x => x.AuthorFirstName)
                        .ThenBy(x => x.Title)
                         .ToList();
            return books;
        }

        public Task<List<Book>> GetAllBookDetailsSort2Async()
        {
            throw new NotImplementedException();
        }

        public async Task LoadBookDataAsync(int count)
        {
            List<Book> books = Builder<Book>.CreateListOfSize(count)
                                .All()
                                .With(p => p.Publisher = Faker.Lorem.Word())
                                .With(p => p.AuthorLastName = Faker.Name.LastName())
                                .With(p => p.AuthorFirstName = Faker.Name.FirstName())
                                .With(p => p.Title = Faker.Lorem.Word())
                                .With(p => p.Price = Faker.Number.RandomNumber(500, 9999))
                                .Build()
                                .ToList();
            await this._bookRepository.LoadBookDataAsync(books);
        }
    }
}