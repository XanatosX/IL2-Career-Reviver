using IL2CarrerReviverModel.Data;
using IL2CarrerReviverModel.Data.Gateways;
using IL2CarrerReviverModel.Data.Repositories;
using IL2CarrerReviverModel.Models;
using IL2CarrerReviverModel.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IL2CarrerReviverModel.DependencyInjection;
public static class ModelDependenciesExtension
{
    public static IServiceCollection AddModelDependencies(this IServiceCollection collection)
    {
        return collection.AddDbContextFactory<IlTwoDatabaseContext>();
    }

    public static IServiceCollection AddDbGateways(this IServiceCollection collection)
    {
        return collection.AddRepositories()
                         .AddSingleton<ICareerGateway, CareerGateway>()
                         .AddSingleton<IPilotGateway, PilotGateway>();
    }

    private static IServiceCollection AddRepositories(this IServiceCollection collection)
    {
        return collection.AddSingleton<IRepository<Career, long>, CareerRepository>()
                         .AddSingleton<IRepository<Pilot, long>, PilotRepository>();
    }

    public static IServiceCollection AddAdditionalServices(this IServiceCollection collection)
    {
        return collection.AddSingleton<IByteArrayToDateTimeService, DefaultByteArrayToDateTimeService>()
                         .AddSingleton<IPilotStateService, DefaultPilotStateService>();
    }




}
