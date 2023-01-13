using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL2CarrerReviverConsole.DepedencyInjection;
internal class DepedencyResolver
{
    private readonly IServiceProvider serviceProvider;

    public DepedencyResolver(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public T? GetService<T>()
    {
        return serviceProvider.GetService<T>();
    }

    public object? GetService<T>(Type type)
    {
        return serviceProvider.GetService(type);
    }
}
