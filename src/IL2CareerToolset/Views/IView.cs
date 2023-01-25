using Spectre.Console.Rendering;

namespace IL2CarrerReviverConsole.Views;
internal interface IView<in T>
{
    IRenderable GetView(T entity);
}
