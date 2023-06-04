using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Knila.BookStore.Infrastructure.DbConnection
{
    public class DapperConnectionProvider : IDapperConnectionProvider
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperConnectionProvider(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._connectionString = this._configuration["SQLConnectionString"];
        }

        public IDbConnection Connect()
            => new Microsoft.Data.SqlClient.SqlConnection(this._connectionString);
    }
}