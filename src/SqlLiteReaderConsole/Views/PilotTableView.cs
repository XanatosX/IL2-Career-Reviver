using IL2CarrerReviverModel.Models;
using IL2CarrerReviverModel.Services;
using Spectre.Console;
using Spectre.Console.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL2CarrerReviverConsole.Views;
internal class PilotTableView : IView<IEnumerable<Pilot>>
{
    private readonly IPilotStateService pilotStateService;

    public PilotTableView(IPilotStateService pilotStateService)
    {
        this.pilotStateService = pilotStateService;
    }

    public IRenderable GetView(IEnumerable<Pilot> entity)
    {
        Table table = new Table();
        table.AddColumn("Database ID");
        table.AddColumn("First Name");
        table.AddColumn("Last Name");
        table.AddColumn("Alive");
        table.AddColumn("Airfield");

        foreach (Pilot pilot in entity)
        {
            table.AddRow(pilot.Id.ToString(), pilot.Name, pilot.LastName, pilotStateService.PilotIsAlive(pilot.State).ToString(), pilot.Squadron.Airfield);
        }
        return table;
    }
}
