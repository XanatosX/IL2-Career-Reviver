using IL2CareerModel.Data.Gateways;
using IL2CarrerReviverModel.Models;

namespace IL2CarrerReviverModel.Data.Gateways;

/// <summary>
/// The gateway to change the pilot table on the database
/// </summary>
public interface IPilotGateway : IReadGateway<Pilot>, IWriteGateway<Pilot>
{
}
