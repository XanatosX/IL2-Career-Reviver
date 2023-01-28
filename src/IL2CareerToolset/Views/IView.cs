using Spectre.Console.Rendering;

namespace IL2CareerToolset.Views;
internal interface IView<in T>
{
    IRenderable GetView(T entity);
}
