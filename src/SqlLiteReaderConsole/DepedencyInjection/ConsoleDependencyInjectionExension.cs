using IL2CarrerReviverConsole.Services;
using IL2CarrerReviverConsole.Views;
using IL2CarrerReviverModel.Data;
using IL2CarrerReviverModel.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL2CarrerReviverConsole.DepedencyInjection;
internal static class ConsoleDependencyInjectionExension
{
    public static IServiceCollection AddViews(this IServiceCollection collection)
    {
        return collection.AddSingleton<DepedencyResolver>()
                         .AddSingleton<ViewFactory>()
                         .AddTransient<PilotView>()
                         .AddTransient<PilotTableView>();
    }

    public static IServiceCollection AddBaseServices(this IServiceCollection collection)
    {
        return collection.AddSingleton<ResourceFileReader>()
                         .AddSingleton<ISettingsService, SettingsService>()
                         .AddSingleton<PathService>()
                         .AddSingleton<IDatabaseConnectionStringService, DatabaseConnectionStringBridgeService>();
    }
}
