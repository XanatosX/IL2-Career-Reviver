using IL2CareerModel.Services;
using IL2CareerToolset.Enums;
using IL2CareerToolset.Services;
using IL2CareerToolset.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace IL2CareerToolset.DependencyInjection;
internal static class ConsoleDependencyInjectionExtension
{
    public static IServiceCollection AddViews(this IServiceCollection collection)
    {
        return collection.AddSingleton<DependencyResolver>()
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
                         .AddSingleton<ISettingsFolderBridge, SettingsFolderBridge>()
                         .AddSingleton<OSInteractionService>()
                         .AddSingleton<IConfiguration>(provider =>
                         {
                             var reader = provider.GetRequiredService<ResourceFileReader>();
                             return new ConfigurationBuilder().AddJsonStream(reader.GetResourceStream("AppSettings.json"))
                                                              .Build();
                         });
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
