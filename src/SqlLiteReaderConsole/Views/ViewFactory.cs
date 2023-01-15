using IL2CarrerReviverConsole.DepedencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL2CarrerReviverConsole.Views;
internal class ViewFactory
{
    private readonly DepedencyResolver depedencyResolver;
    private readonly ILogger<ViewFactory> logger;

    public ViewFactory(DepedencyResolver depedencyResolver, ILogger<ViewFactory> logger)
    {
        this.depedencyResolver = depedencyResolver;
        this.logger = logger;
    }

    public T? CreateView<T>()
    {
        logger.LogDebug($"Creating view for {typeof(T)}");
        return depedencyResolver.GetService<T>();
    }
}
