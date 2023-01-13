using IL2CarrerReviverModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL2CarrerReviverModel.Services;
public interface IPilotStateService
{
    bool PilotIsAlive(Pilot pilot) => PilotIsAlive(pilot.State);

    bool PilotIsAlive(long pilotState);
}
