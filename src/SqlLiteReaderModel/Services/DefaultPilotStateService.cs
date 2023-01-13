using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL2CarrerReviverModel.Services;
internal class DefaultPilotStateService : IPilotStateService
{
    public bool PilotIsAlive(long pilotState)
    {
        return pilotState != 2;
    }
}
