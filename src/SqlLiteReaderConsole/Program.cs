using IL2CarrerReviverConsole.Commands.Cli;
using IL2CarrerReviverConsole.DepedencyInjection;
using IL2CarrerReviverModel.Data;
using IL2CarrerReviverModel.Data.Gateways;
using IL2CarrerReviverModel.DependencyInjection;
using IL2CarrerReviverModel.Models;
using IL2CarrerReviverModel.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
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
            var collection = new ServiceCollection().AddModelDependencies()
                                                    .AddDbGateways()
                                                    .AddViews()
                                                    .AddAdditionalServices();
            var typeRegistrar = new DependencyInjectionRegistrar(collection);
            var app = new CommandApp(typeRegistrar);

            app.Configure(config =>
            {
                config.AddBranch("list", listConfig =>
                {
                    listConfig.AddCommand<GetPilotsCommand>("pilot");
                });
            });
            return app.Run(args);
            var services = new ServiceCollection().AddModelDependencies()
                                                  .AddDbGateways()
                                                  .AddAdditionalServices()
                                                  .BuildServiceProvider();


            var gateway = services.GetRequiredService<IPilotGateway>();
            var dateTimeService = services.GetRequiredService<IByteArrayToDateTimeService>();



            var results = gateway.GetAll().First();

            var time = dateTimeService.GetDateTime(results.InsDate ?? Array.Empty<byte>());


            //List<IL2CarrerReviverModel.Models.Pilot> playerPilots = careers.Select(career => career.Player).ToList();

        }
    }
}