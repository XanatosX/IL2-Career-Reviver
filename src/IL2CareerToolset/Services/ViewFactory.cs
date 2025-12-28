using IL2CareerToolset.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace IL2CareerToolset.Services;
internal class ViewFactory
{
    private readonly DependencyResolver dependencyResolver;
    private readonly IMemoryCache cache;
    private readonly ILogger<ViewFactory> logger;

    public ViewFactory(DependencyResolver dependencyResolver, IMemoryCache cache, ILogger<ViewFactory> logger)
    {
        this.dependencyResolver = dependencyResolver;
        this.cache = cache;
        this.logger = logger;
    }

    public T? CreateView<T>() => CreateView<T>(true);

    public T? CreateView<T>(bool allowGettingCached)
    {
        logger.LogDebug($"Requesting view for {typeof(T)}");
        return allowGettingCached ? cache.GetOrCreate(typeof(T), entry => CreateViewOfType<T>()) : CreateViewOfType<T>();
    }

    private T? CreateViewOfType<T>()
    {
        logger.LogDebug($"Create view for {typeof(T)}");
        return dependencyResolver.GetService<T>();
    }
}
