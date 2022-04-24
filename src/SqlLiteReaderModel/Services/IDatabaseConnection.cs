using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace src.SqlLiteReaderModel.Services
{
    public interface IDatabaseConnection
    {
        void SetConnectionString(string connectionString);

        DbConnection GetConnection();

        void CloseConnection();

        DbCommand CreateCommand(string sqlCommand);

        DbParameter GetParameter(DbCommand command, string parameter, object value);

        void AddParameter(DbCommand command, string parameter, object value);
    }
}