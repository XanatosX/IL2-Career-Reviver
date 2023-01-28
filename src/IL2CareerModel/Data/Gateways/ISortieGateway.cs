using IL2CareerModel.Models;

namespace IL2CareerModel.Data.Gateways;

/// <summary>
/// The gateway to change the sortie table on the database
/// </summary>
public interface ISortieGateway : IReadGateway<Sortie>, IWriteGateway<Sortie>
{
}

