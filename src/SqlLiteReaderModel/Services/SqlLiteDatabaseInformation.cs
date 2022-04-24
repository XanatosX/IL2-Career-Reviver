using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace src.SqlLiteReaderModel.Services
{
    public class SqlLiteDatabaseInformation : IDatabaseInformation
    {
        private const string DATABASE_VERSION_STATEMENT = "SELECT SQLITE_VERSION();";

        private const string DATABASE_TABLES_STATEMENT = "SELECT name FROM sqlite_master WHERE type = 'table' ORDER BY 1;";

        private const string DATABASE_TABLE_COLUMNS_STATEMENT = "SELECT name FROM pragma_table_info(@tableName) ORDER BY cid";

        private readonly IDatabaseConnection databaseConnection;

        public SqlLiteDatabaseInformation(IDatabaseConnection databaseConnection)
        {
            this.databaseConnection = databaseConnection;
        }

        private DbConnection GetOpenConnection()
        {
            DbConnection con = databaseConnection.GetConnection();
            if (con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            return con;
        }

        public string GetDatabaseVersion()
        {
            string? returnVersion = string.Empty;
            if (databaseConnection == null)
            {
                return returnVersion;
            }
            DbConnection con = GetOpenConnection();
            DbCommand command = con.CreateCommand();
            command.CommandText = DATABASE_VERSION_STATEMENT;

            returnVersion = command.ExecuteScalar()?.ToString();
            con.Close();
            return returnVersion ?? string.Empty;
        }

        public string[] GetTableColumns(string tableName)
        {
            string[] returnStrings = new string[0];
            using (DbConnection con = GetOpenConnection())
            {
                DbCommand command = databaseConnection.CreateCommand(DATABASE_TABLE_COLUMNS_STATEMENT);
                databaseConnection.AddParameter(command, "@tableName", tableName);
                command.Prepare();

                using (DbDataReader reader = command.ExecuteReader())
                {
                    List<string> readList = new List<string>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            readList.Add(reader.GetString(0));
                        }
                    }
                    returnStrings = readList.Where(name => !string.IsNullOrEmpty(name)).ToArray();
                }
            }
            return returnStrings;
        }

        public string[] GetTables()
        {
            string[] returnStrings = new string[0];
            using (DbConnection con = GetOpenConnection())
            {
                DbCommand command = con.CreateCommand();
                command.CommandText = DATABASE_TABLES_STATEMENT;
                using (DbDataReader reader = command.ExecuteReader())
                {
                    List<string> readList = new List<string>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            readList.Add(reader.GetString(0));
                        }
                    }
                    returnStrings = readList.Where(name => !string.IsNullOrEmpty(name)).ToArray();
                }
            }

            return returnStrings;
        }
    }
}