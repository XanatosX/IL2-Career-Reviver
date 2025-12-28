using IL2CareerModel.DependencyInjection;
using IL2CareerToolset.Commands.Cli;
using IL2CareerToolset.Commands.Cli.Entity;
using IL2CareerToolset.Commands.Cli.Save;
using IL2CareerToolset.Commands.Cli.Web;
using IL2CareerToolset.DependencyInjection;
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
                                                        var databasePath = provider.GetRequiredService<ISettingsService>().GetSettings()?.DatabasePath;
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
                    settingConfig.SetDescription("Commands to find the path to the game or set the log level for the application");
                    settingConfig.AddCommand<AutomaticallyDetectDatabaseCommand>("auto")
                                 .WithExample(new[] {"settings", "auto"});
                    settingConfig.AddCommand<ManuelDatabaseCommand>("manuel")
                                 .WithExample(new[] { "settings", "manuel" })
                                 .WithExample(new[] {"settings", "manuel", @"path\to\game\root\directory"});
                    settingConfig.AddCommand<SetLogLevelCommand>("loglevel")
                                 .WithExample(new[] {"settings", "loglevel", "Information"});
                });
                config.AddBranch("save", databaseConfig =>
                {
                    databaseConfig.SetDescription("Commands to manage backups or interact with the game database");
                    databaseConfig.AddBranch("backup", backupConfig =>
                    {
                        backupConfig.SetDescription("Commands to create, delete, list and restore backups of the game database");
                        backupConfig.AddCommand<CreateBackupCommand>("create")
                                    .WithExample(new[] { "save", "backup", "create" })
                                    .WithExample(new[] {"save", "backup", "create", "\"Backup name\""});;
                        backupConfig.AddCommand<DeleteBackupsCommand>("delete")
                                    .WithExample(new[] { "save", "backup", "delete" })
                                    .WithExample(new[] { "save", "backup", "delete", "1cdb087d-3d64-47bd-adb1-648f21cf6f69"})
                                    .WithExample(new[] { "save", "backup", "delete", "--all"});
                        backupConfig.AddCommand<ListBackupsCommand>("list")
                                    .WithExample(new[] { "save", "backup", "list" });
                        backupConfig.AddCommand<ChangeBackupNameCommand>("rename")
                                    .WithExample(new[] { "save", "backup", "rename" })
                                    .WithExample(new[] { "save", "backup", "rename", "1cdb087d-3d64-47bd-adb1-648f21cf6f69", "\"New name\"" });
                        backupConfig.AddCommand<RestoreBackupCommand>("restore")
                                    .WithExample(new[] { "save", "backup", "restore" })
                                    .WithExample(new[] { "save", "backup", "restore", "1cdb087d-3d64-47bd-adb1-648f21cf6f69" });
                    });

                    databaseConfig.AddBranch("game", listConfig =>
                    {
                        listConfig.SetDescription("Commands to list pilots or revive them");
                        listConfig.AddCommand<GetPilotsCommand>("pilot")
                                  .WithExample(new[] { "save", "game", "pilot" })
                                  .WithExample(new[] { "save", "game", "pilot", "--player" })
                                  .WithExample(new[] { "save", "game", "pilot", "Pilot name" })
                                  .WithExample(new[] { "save", "game", "pilot", "Pilot name", "--player" });
                        listConfig.AddCommand<RevivePilotCommand>("revive")
                                  .WithExample(new[] { "save", "game", "revive" })
                                  .WithExample(new[] { "save", "game", "revive", "--ironman" });
                    });

                });
                config.AddBranch("app", repo =>
                {
                    repo.SetDescription("Commands for interacting with the application like open the repository, an issues or get some help");
                    repo.AddCommand<OpenRepositoryCommand>("open")
                        .WithAlias("repo")
                        .WithExample(new[] { "app", "open" });
                    repo.AddCommand<OpenIssueCommand>("issue")
                        .WithAlias("bug")
                        .WithExample(new[] { "app", "issue" });
                    repo.AddCommand<OpenHelpCommand>("help")
                        .WithExample(new[] { "app", "help" });
                });
            });
            int returnCode = app.Run(args);
            return returnCode;

        }
    }
}
