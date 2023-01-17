using IL2CarrerReviverConsole.Commands.Cli;
using IL2CarrerReviverConsole.Commands.Cli.Save;
using IL2CarrerReviverConsole.DepedencyInjection;
using IL2CarrerReviverConsole.Services;
using IL2CarrerReviverModel.Data;
using IL2CarrerReviverModel.Data.Gateways;
using IL2CarrerReviverModel.DependencyInjection;
using IL2CarrerReviverModel.Models;
using IL2CarrerReviverModel.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Cli.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.SqlLiteReaderConsole
{
    public class Program
    {
        public static int Main(params string[] args)
        {
            var collection = new ServiceCollection().AddBaseServices()
                                                    .AddModelDependencies()
                                                    .AddDbGateways()
                                                    .AddViews()
                                                    .AddAdditionalServices();
            var typeRegistrar = new DependencyInjectionRegistrar(collection);
            var app = new CommandApp(typeRegistrar);

            var resourceReader = collection.BuildServiceProvider().GetRequiredService<ResourceFileReader>();
            string name = resourceReader.GetResourceContent("AppName.txt");
            AnsiConsole.Write(new FigletText($"{name}").Centered().Color(Color.Green));
            app.Configure(config =>
            {
                config.AddBranch("settings", settingConfig =>
                {
                    settingConfig.AddCommand<AutomaticallyDetectDatabaseCommand>("auto");
                    settingConfig.AddCommand<ManuellDatabaseCommand>("manuell");
                    settingConfig.AddCommand<SetLogLevelCommand>("loglevel");
                });
                config.AddBranch("list", listConfig =>
                {
                    listConfig.AddCommand<GetPilotsCommand>("pilot");
                });
                config.AddBranch("save", databaseConfig =>
                {
                    databaseConfig.AddBranch("backup", backupConfig =>
                    {
                        backupConfig.AddCommand<CreateBackupCommand>("create");
                        backupConfig.AddCommand<DeleteBackupsCommand>("delete");
                        backupConfig.AddCommand<ListBackupsCommand>("list");
                        backupConfig.AddCommand<ChangeBackupNameCommand>("rename");
                        backupConfig.AddCommand<RestoreBackupCommand>("restore");
                    });

                });
            });
            int returnCode = app.Run(args);
            return returnCode;

        }
    }
}