using Castle.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knila.BookStore.Infrastructure.DbConnection
{
    public class DapperConnectionProvider : IDapperConnectionProvider
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperConnectionProvider(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\vijir\\githubsource\\Knila.BookStore\\Knila.BookStore\\Database\\KnilaBookStore.mdf;Integrated Security=True;Connect Timeout=30";
        }

        public IDbConnection Connect()
            => new Microsoft.Data.SqlClient.SqlConnection(_connectionString);
    }
}