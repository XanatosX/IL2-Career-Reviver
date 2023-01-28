using IL2CareerModel.DependencyInjection;
using IL2CareerToolset.Commands.Cli;
using IL2CareerToolset.Commands.Cli.Entity;
using IL2CareerToolset.Commands.Cli.Save;
using IL2CareerToolset.Commands.Cli.Web;
using IL2CareerToolset.DepedencyInjection;
using IL2CareerToolset.Services;
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
                                                    .AddModelDependencies((settings, provider) =>
                                                    {
                                                        var databasePath = provider.GetService<ISettingsService>()?.GetSettings()?.DatabasePath;
                                                        settings.DatabasePath = databasePath;
                                                    })
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
                        listConfig.AddCommand<RevivePilotCommand>("revive");
                    });

                });
                config.AddBranch("app", repo =>
                {
                    repo.AddCommand<OpenRepositoryCommand>("open");
                    repo.AddCommand<OpenIssueCommand>("issue");
                    repo.AddCommand<OpenHelpCommand>("help");
                });
            });
            int returnCode = app.Run(args);
            return returnCode;

        }
    }
}
