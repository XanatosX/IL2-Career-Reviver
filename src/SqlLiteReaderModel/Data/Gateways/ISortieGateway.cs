using IL2CarrerReviverModel.Models;

namespace IL2CarrerReviverModel.Data.Gateways;
public interface ISortieGateway
{
    IEnumerable<Sortie> GetAll() => GetAll(_ => true);

    IEnumerable<Sortie> GetAll(Func<Sortie, bool> filter);

    Sortie? GetById(long id);

    bool DeleteById(long id);
}

