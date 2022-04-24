using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace src.SqlLiteReaderModel.Services
{
    public class SqlLiteConnection : IDatabaseConnection
    {
        private string? connectionString;

        private DbConnection? connection;

        public void SetConnectionString(string connectionString)
        {
            if (File.Exists(connectionString))
            {
                connectionString = string.Concat("URI=file:", connectionString);
            }
            this.connectionString = connectionString;
            CloseConnection();
        }



        public DbParameter GetParameter(DbCommand command, string parameter, object value)
        {
            DbParameter returnParameter = command.CreateParameter();
            returnParameter.ParameterName = parameter;
            returnParameter.Value = value;

            return returnParameter;
        }

        public void AddParameter(DbCommand command, string parameter, object value)
        {
            command.Parameters.Add(GetParameter(command, parameter, value));
        }

        public void CloseConnection()
        {
            if (connection == null)
            {
                return;
            }
            connection.Close();
        }

        public DbCommand CreateCommand(string sqlCommand)
        {
            DbCommand returnCommand = GetConnection().CreateCommand();
            returnCommand.CommandText = sqlCommand;
            return returnCommand;
        }

        public DbConnection GetConnection()
        {
            if (connectionString == null)
            {
                return null;
            }
            if (connection == null || connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection = new SQLiteConnection(connectionString);
                connection.Disposed += (sender, data) =>
                {
                    connection = null;
                };
            }
            return connection;
        }


    }
}