using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IL2CareerToolset.DependencyInjection;
internal class DependencyResolver
{
    private readonly IServiceProvider serviceProvider;
    private readonly ILogger<DependencyResolver> logger;

    public DependencyResolver(IServiceProvider serviceProvider, ILogger<DependencyResolver> logger)
    {
        this.serviceProvider = serviceProvider;
        this.logger = logger;
    }

    public T? GetService<T>()
    {
        logger.LogInformation($"Get generic Service for {typeof(T)}");
        return serviceProvider.GetService<T>();
    }

    public object? GetService(Type type)
    {
        logger.LogInformation($"Get Service for {type}");
        return serviceProvider.GetService(type);
    }
}
