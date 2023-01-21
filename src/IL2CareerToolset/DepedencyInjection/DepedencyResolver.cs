using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IL2CarrerReviverConsole.DepedencyInjection;
internal class DepedencyResolver
{
    private readonly IServiceProvider serviceProvider;
    private readonly ILogger<DepedencyResolver> logger;

    public DepedencyResolver(IServiceProvider serviceProvider, ILogger<DepedencyResolver> logger)
    {
        this.serviceProvider = serviceProvider;
        this.logger = logger;
    }

    public T? GetService<T>()
    {
        logger.LogInformation($"Get generic Service for {typeof(T)}");
        return serviceProvider.GetService<T>();
    }

    public object? GetService<T>(Type type)
    {
        logger.LogInformation($"Get Service for {type}");
        return serviceProvider.GetService(type);
    }
}
