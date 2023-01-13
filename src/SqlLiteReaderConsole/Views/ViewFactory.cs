using IL2CarrerReviverConsole.DepedencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL2CarrerReviverConsole.Views;
internal class ViewFactory
{
    private readonly DepedencyResolver depedencyResolver;

    public ViewFactory(DepedencyResolver depedencyResolver)
    {
        this.depedencyResolver = depedencyResolver;
    }

    public T? CreateView<T>()
    {
        return depedencyResolver.GetService<T>();
    }
}
