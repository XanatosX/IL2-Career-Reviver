using IL2CareerModel.Data.Gateways;
using IL2CarrerReviverModel.Models;

namespace IL2CarrerReviverModel.Data.Gateways;

/// <summary>
/// The gateway to change the career table on the database
/// </summary>
public interface ICareerGateway : IReadGateway<Career>, IWriteGateway<Career>
{
}
