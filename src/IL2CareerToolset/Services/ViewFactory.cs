using IL2CareerToolset.DepedencyInjection;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace IL2CareerToolset.Services;
internal class ViewFactory
{
    private readonly DepedencyResolver depedencyResolver;
    private readonly IMemoryCache cache;
    private readonly ILogger<ViewFactory> logger;

    public ViewFactory(DepedencyResolver depedencyResolver, IMemoryCache cache, ILogger<ViewFactory> logger)
    {
        this.depedencyResolver = depedencyResolver;
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
        return depedencyResolver.GetService<T>();
    }
}
