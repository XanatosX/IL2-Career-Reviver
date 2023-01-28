using IL2CareerModel.Models;
using IL2CareerModel.Services;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace IL2CareerToolset.Views;
internal class PilotView : IView<Pilot>
{
    private readonly IPilotStateService pilotStateService;

    public PilotView(IPilotStateService pilotStateService)
    {
        this.pilotStateService = pilotStateService;
    }
    public IRenderable GetView(Pilot entity)
    {
        bool alive = pilotStateService.PilotIsAlive(entity);
        return new Rows(
            new Text($"Id: {entity.Id}"),
            new Text($"Name: {entity.Name} {entity.LastName}"),
            new Text($"Alive: {alive}"),
            new Text($"Airfield: {entity.Squadron.Airfield}"));
    }
}
