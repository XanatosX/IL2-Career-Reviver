using IL2CarrerReviverModel.Models;

namespace IL2CarrerReviverModel.Services;
public interface IPilotStateService
{
    bool PilotIsAlive(Pilot pilot) => PilotIsAlive(pilot.State);

    bool PilotIsAlive(long pilotState);
}
