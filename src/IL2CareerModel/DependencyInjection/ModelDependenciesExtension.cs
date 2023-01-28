using IL2CareerModel.Data;
using IL2CareerModel.Data.Gateways;
using IL2CareerModel.Data.Gateways.Implementations;
using IL2CareerModel.Data.Repositories;
using IL2CareerModel.Models;
using IL2CareerModel.Models.Database;
using IL2CareerModel.Services;
using IL2CareerModel.Services.SaveGame;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IL2CareerModel.DependencyInjection;

/// <summary>
/// Extension class to provide methods for dependency injection
/// </summary>
public static class ModelDependenciesExtension
{
    /// <summary>
    /// Add all the dependencies for the model
    /// </summary>
    /// <param name="collection">The collection with all the already registered services</param>
    /// <param name="createDatabaseSettings">THe method used to create the database settings</param>
    /// <returns>The new collection with additional services</returns>
    public static IServiceCollection AddModelDependencies(this IServiceCollection collection, Action<DatabaseSettings, IServiceProvider> createDatabaseSettings)
    {
        DatabaseSettings settings = new DatabaseSettings();
        createDatabaseSettings(settings, collection.BuildServiceProvider());
        return collection.AddDbContextFactory<IlTwoDatabaseContext>(options =>
        {
            options.UseSqlite(settings.ConnectionString);
        }).AddSingleton(_ => settings);
    }

    /// <summary>
    /// Add all the dependencies for the gateways
    /// </summary>
    /// <param name="collection">The collection to update</param>
    /// <returns>The updated collection</returns>
    public static IServiceCollection AddDbGateways(this IServiceCollection collection)
    {
        return collection.AddRepositories()
                         .AddSingleton<ICareerGateway, CareerGateway>()
                         .AddSingleton<IPilotGateway, PilotGateway>()
                         .AddSingleton<IMissionGateway, MissionGateway>()
                         .AddSingleton<ISortieGateway, SortieGateway>();
    }

    /// <summary>
    /// Add all the repositories for the gateways
    /// </summary>
    /// <param name="collection">The collection to update</param>
    /// <returns>The updated collection</returns>
    private static IServiceCollection AddRepositories(this IServiceCollection collection)
    {
        return collection.AddSingleton<IBaseRepository<Career, long>, CareerRepository>()
                         .AddSingleton<IBaseRepository<Pilot, long>, PilotRepository>()
                         .AddSingleton<IBaseRepository<Mission, long>, MissionRepository>()
                         .AddSingleton<IBaseRepository<Sortie, long>, SortieRepository>();

    }

    /// <summary>
    /// Add additional services to the dependency manager
    /// </summary>
    /// <param name="collection">The collection to update</param>
    /// <returns>The updated collection</returns>
    public static IServiceCollection AddAdditionalServices(this IServiceCollection collection)
    {
        return collection.AddSingleton<IByteArrayToDateTimeService, DefaultByteArrayToDateTimeService>()
                         .AddSingleton<IPilotStateService, DefaultPilotStateService>()
                         .AddSingleton<ISavegameLocatorService, AutomaticSteamSavegameSearchingService>()
                         .AddSingleton<IGamePathValidationService, DefaultGamePathValidationService>()
                         .AddSingleton<IDatabaseBackupService, DatabaseBackupService>()
                         .AddSingleton<IFileChecksumService, Md5ChecksumService>()
                         .AddSingleton<IGamePathValidationService, DefaultGamePathValidationService>();
    }




}
