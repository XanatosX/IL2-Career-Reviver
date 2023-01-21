using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL2CarrerReviverConsole.Services;
internal class DatabaseBackupService
{
    private readonly ILogger<DatabaseBackupService> logger;

    public DatabaseBackupService(PathService pathService, ILogger<DatabaseBackupService> logger)
    {
        this.logger = logger;
    }


}
