using IL2CarrerReviverModel.Models;

namespace IL2CarrerReviverModel.Data.Gateways;
public interface IMissionGateway
{
    IEnumerable<Mission> GetAll() => GetAll(_ => true);

    IEnumerable<Mission> GetAll(Func<Mission, bool> filter);

    Mission? GetById(long id);

    bool DeleteById(long id);
}
