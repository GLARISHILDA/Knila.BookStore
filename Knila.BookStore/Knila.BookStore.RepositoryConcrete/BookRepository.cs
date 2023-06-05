using Dapper;
using Knila.BookStore.Domain;
using Knila.BookStore.Infrastructure.DbConnection;
using Knila.BookStore.RepositoryInterface;
using System.Data;
using System;

namespace Knila.BookStore.RepositoryConcrete
{
    public class BookRepository : IBookRepository
    {
        private readonly IDapperConnectionProvider _dapperConnectionProvider;

        public BookRepository(IDapperConnectionProvider dapperConnectionProvider)
        {
            this._dapperConnectionProvider = dapperConnectionProvider;
        }

        public async Task<List<Book>> GetAllBookDetailsAsync()
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

        public async Task LoadBookDataAsync(List<Book> books)
        {
            string sql = "[dbo].[P_LoadBookData]";

            using (var connection = this._dapperConnectionProvider.Connect())
            {
                var table = new DataTable();
                table.Columns.Add("Publisher", typeof(string));
                table.Columns.Add("Title", typeof(string));
                table.Columns.Add("AuthorLastName", typeof(string));
                table.Columns.Add("AuthorFirstName", typeof(string));
                table.Columns.Add("Price", typeof(decimal));

                foreach (var book in books)
                {
                    var row = table.NewRow();
                    row["Publisher"] = book.Publisher;
                    row["Title"] = book.Title;
                    row["AuthorLastName"] = book.AuthorLastName;
                    row["AuthorFirstName"] = book.AuthorFirstName;
                    row["Price"] = book.Price;
                    table.Rows.Add(row);
                }

                var result = await connection.ExecuteAsync(sql, new
                {
                    books = table.AsTableValuedParameter("T_Book")
                }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<double> GetTotalBookPriceAsync()
        {
            double sumOfBookPrice = 0.00;

            string sql = "select ISNULL (sum (price),0.00) SumOfBookPrice from book";

            using (var connection = this._dapperConnectionProvider.Connect())
            {
                sumOfBookPrice = await connection
                                 .QueryFirstOrDefaultAsync<double>(sql, commandType: System.Data.CommandType.Text);
            }
            return sumOfBookPrice;
        }

        public async Task<int> GetTotalNumberOfBooksAsync()
        {
            int sumOfBooks = 0;

            string sql = "select ISNULL (sum (BookId),0) SumOfBooks from book";

            using (var connection = this._dapperConnectionProvider.Connect())
            {
                sumOfBooks = await connection
                                 .QueryFirstOrDefaultAsync<int>(sql, commandType: System.Data.CommandType.Text);
            }
            return sumOfBooks;
        }
    }
}