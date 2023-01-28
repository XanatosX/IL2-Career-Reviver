using IL2CareerModel.Models;

namespace IL2CareerModel.Services;
public interface IPilotStateService
{
    bool PilotIsAlive(Pilot pilot) => PilotIsAlive(pilot.State);

    bool PilotIsAlive(long pilotState);
}
