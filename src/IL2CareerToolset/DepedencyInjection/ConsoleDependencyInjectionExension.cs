using IL2CareerToolset.Enums;
using IL2CareerToolset.Services;
using IL2CareerToolset.Views;
using IL2CarrerReviverModel.Services;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace IL2CareerToolset.DepedencyInjection;
internal static class ConsoleDependencyInjectionExension
{
    public static IServiceCollection AddViews(this IServiceCollection collection)
    {
        return collection.AddSingleton<DepedencyResolver>()
                         .AddSingleton<ViewFactory>()
                         .AddTransient<PilotView>()
                         .AddTransient<PilotTableView>()
                         .AddTransient<BackupTableView>();
    }

    public static IServiceCollection AddBaseServices(this IServiceCollection collection)
    {
        return collection.AddSingleton<ResourceFileReader>()
                         .AddMemoryCache()
                         .AddSingleton<ISettingsService, SettingsService>()
                         .AddSingleton<PathService>()
                         .AddSingleton<DefaultLogLevelMapper>()
                         .AddLogging(config =>
                         {
                             config.AddSerilog(CreateLoggerConfig(collection));
                         })
                         .AddSingleton<ISettingsFolderBridge, SettingsFolderBridge>();
    }

    private static Serilog.ILogger CreateLoggerConfig(IServiceCollection collection)
    {
        var provider = collection.BuildServiceProvider();
        LogSeverity logging = provider.GetRequiredService<ISettingsService>().GetSettings()?.RealLogLevel ?? LogSeverity.Warning;

        string settingsPath = provider.GetRequiredService<PathService>().GetAndCreateSettingFolder();

        string fileTemplate = "{Timestamp} [{Level:w4}] [{SourceContext}] {Message:l}{NewLine}{Exception}";
        return new LoggerConfiguration().MinimumLevel.Debug()
                                        .WriteTo.File(Path.Combine(settingsPath, "application.log"),
                                                      outputTemplate: fileTemplate,
                                                      rollingInterval: RollingInterval.Day,
                                                      restrictedToMinimumLevel: provider.GetRequiredService<DefaultLogLevelMapper>().GetLogLevel(logging))
                                        .Enrich.FromLogContext()
                                        .CreateLogger();
    }
}
