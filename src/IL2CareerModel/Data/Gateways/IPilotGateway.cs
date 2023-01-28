using IL2CareerModel.Data.Gateways;
using IL2CareerModel.Models;

namespace IL2CareerModel.Data.Gateways;

/// <summary>
/// The gateway to change the pilot table on the database
/// </summary>
public interface IPilotGateway : IReadGateway<Pilot>, IWriteGateway<Pilot>
{
}
