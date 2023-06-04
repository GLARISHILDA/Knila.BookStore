using Knila.BookStore.Domain;

namespace Knila.BookStore.RepositoryInterface
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBookDetailsSort1Async();

        Task<List<Book>> GetAllBookDetailsSort2Async();
    }
}