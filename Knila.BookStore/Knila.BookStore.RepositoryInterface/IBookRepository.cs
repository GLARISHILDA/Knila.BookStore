using Insight.Database;
using Knila.BookStore.Domain;

namespace Knila.BookStore.RepositoryInterface
{
    public interface IBookRepository
    {
        [Sql(@"[dbo].[P_GetAllBookDetails]")]
        Task<List<Book>> GetAllBookDetailsAsync();

        Task LoadBookDataAsync(List<Book> books);

        [Sql(@"select ISNULL (sum (price),0.00) SumOfBookPrice from book")]
        Task<double> GetTotalBookPriceAsync();

        [Sql(@"select ISNULL (count (BookId),0) SumOfBooks from book")]
        Task<int> GetTotalNumberOfBooksAsync();

        [Sql(@"[dbo].[P_GetLast5BookDetails]")]
        Task<List<Book>> GetLast5BookDetailsAsync();
    }
}