using Knila.BookStore.Domain;

namespace Knila.BookStore.RepositoryInterface
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBookDetailsAsync();

        Task LoadBookDataAsync(List<Book> books);

        Task<double> GetTotalBookPriceAsync();
    }
}