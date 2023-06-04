using Dapper;
using Knila.BookStore.Domain;
using Knila.BookStore.Infrastructure.DbConnection;
using Knila.BookStore.RepositoryInterface;

namespace Knila.BookStore.RepositoryConcrete
{
    public class BookRepository : IBookRepository
    {
        private readonly IDapperConnectionProvider _dapperConnectionProvider;

        public BookRepository(IDapperConnectionProvider dapperConnectionProvider)
        {
            this._dapperConnectionProvider = dapperConnectionProvider;
        }

        public async Task<List<Book>> GetAllBookDetailsSort1Async()
        {
            List<Book> books = new List<Book>();

            string sql = "[dbo].[P_GetAllBookDetails]";

            using (var connection = this._dapperConnectionProvider.Connect())
            {
                var booksTemp = await connection
                                 .QueryAsync<Book>(sql, commandType: System.Data.CommandType.StoredProcedure);

                books = booksTemp.ToList();
            }

            return books;
        }

        public Task<List<Book>> GetAllBookDetailsSort2Async()
        {
            throw new NotImplementedException();
        }
    }
}