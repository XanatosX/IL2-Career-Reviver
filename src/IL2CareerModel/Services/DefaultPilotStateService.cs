namespace IL2CareerModel.Services;
internal class DefaultPilotStateService : IPilotStateService
{
    public bool PilotIsAlive(long pilotState)
    {
        return pilotState != 2;
    }
}
