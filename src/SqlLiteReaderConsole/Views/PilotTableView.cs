using IL2CarrerReviverModel.Models;
using IL2CarrerReviverModel.Services;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace IL2CarrerReviverConsole.Views;
internal class PilotTableView : IView<IEnumerable<Pilot>>
{
    private readonly IPilotStateService pilotStateService;
    private readonly IByteArrayToDateTimeService byteArrayToDateTime;

    public PilotTableView(IPilotStateService pilotStateService, IByteArrayToDateTimeService byteArrayToDateTime)
    {
        this.pilotStateService = pilotStateService;
        this.byteArrayToDateTime = byteArrayToDateTime;
    }

    public IRenderable GetView(IEnumerable<Pilot> entity)
    {
        Table table = new Table();
        table.AddColumn("Database ID");
        table.AddColumn("First Name");
        table.AddColumn("Last Name");
        table.AddColumn("Birth Date (dd.mm.yyyy");
        table.AddColumn("Alive");
        table.AddColumn("Airfield");


        foreach (Pilot pilot in entity.OrderBy(p => p.Id))
        {
            string birthday = byteArrayToDateTime.GetDateTime(pilot.BirthDay ?? Array.Empty<byte>())?.ToString("dd.MM.yyyy") ?? "Unknown";
            table.AddRow(pilot.Id.ToString(), pilot.Name, pilot.LastName, birthday, pilotStateService.PilotIsAlive(pilot.State).ToString(), pilot.Squadron.Airfield);
        }
        return table;
    }
}
