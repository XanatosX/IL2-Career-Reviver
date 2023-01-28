using IL2CareerModel.Models;

namespace IL2CareerModel.Data.Gateways;

/// <summary>
/// The gateway to change the career table on the database
/// </summary>
public interface ICareerGateway : IReadGateway<Career>, IWriteGateway<Career>
{
}
