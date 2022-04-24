using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.SqlLiteReaderModel.Services
{
    public interface IDatabaseInformation
    {
        string GetDatabaseVersion();

        string[] GetTables();

        string[] GetTableColumns(string tableName);

        
    }
}