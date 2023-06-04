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
    }
}