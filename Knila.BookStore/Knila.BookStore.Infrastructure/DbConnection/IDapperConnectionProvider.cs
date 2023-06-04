namespace Knila.BookStore.Infrastructure.DbConnection
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDapperConnectionProvider
    {
        IDbConnection Connect();
    }
}