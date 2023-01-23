using IL2CareerModel.Data.Gateways;
using IL2CarrerReviverModel.Models;

namespace IL2CarrerReviverModel.Data.Gateways;

/// <summary>
/// The gateway to change the sortie table on the database
/// </summary>
public interface ISortieGateway : IReadGateway<Sortie>, IWriteGateway<Sortie>
{
}

