using IL2CarrerReviverConsole.Commands.Cli;
using IL2CarrerReviverConsole.Commands.Cli.Entity;
using IL2CarrerReviverConsole.Commands.Cli.Save;
using IL2CarrerReviverConsole.DepedencyInjection;
using IL2CarrerReviverConsole.Services;
using IL2CarrerReviverModel.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Cli.Extensions.DependencyInjection;

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

                    databaseConfig.AddBranch("game", listConfig =>
                    {
                        listConfig.AddCommand<GetPilotsCommand>("pilot");
                    });

                });
            });
            int returnCode = app.Run(args);
            return returnCode;

        }
    }
}