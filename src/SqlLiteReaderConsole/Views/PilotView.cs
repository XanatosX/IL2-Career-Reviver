﻿using IL2CarrerReviverModel.Models;
using IL2CarrerReviverModel.Services;
using Spectre.Console;
using Spectre.Console.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL2CarrerReviverConsole.Views;
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
