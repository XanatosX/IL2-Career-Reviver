using IL2CarrerReviverConsole.Services;
using IL2CarrerReviverConsole.Views;
using IL2CarrerReviverModel.Services;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

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
                         .AddSingleton<IDatabaseConnectionStringService, DatabaseConnectionStringBridgeService>()
                         .AddLogging(config =>
                         {
                             config.AddSerilog(CreateLoggerConfig(collection));
                         });
    }

    private static Serilog.ILogger CreateLoggerConfig(IServiceCollection collection)
    {
        string settingsPath = collection.BuildServiceProvider().GetRequiredService<PathService>().GetAndCreateSettingFolder();

        var fileTemplate = "{Timestamp} [{Level:w4}] [{SourceContext}] {Message:l}{NewLine}{Exception}";
        return new LoggerConfiguration().WriteTo.File(Path.Combine(settingsPath, "application.log"), outputTemplate: fileTemplate)
                                                .Enrich.FromLogContext()
                                                .CreateLogger();
    }
}
