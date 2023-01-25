using IL2CarrerReviverModel.Data.Gateways;
using IL2CarrerReviverModel.Data.Repositories;
using IL2CarrerReviverModel.Models;

namespace IL2CareerModel.Data.Gateways.Implementations;

/// <summary>
/// The gateway implementation to change the mission table on the database
/// </summary>
internal class MissionGateway : IMissionGateway
{
    /// <summary>
    /// The data repository to use
    /// </summary>
    private readonly IBaseRepository<Mission, long> baseRepository;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="baseRepository">The base repository to use for working with the data</param>
    public MissionGateway(IBaseRepository<Mission, long> baseRepository)
    {
        this.baseRepository = baseRepository;
    }

    public bool Delete(Mission mission)
    {
        return baseRepository.Delete(mission);
    }

    public bool DeleteById(long id)
    {
        return baseRepository.DeleteById(id);
    }

    public IEnumerable<Mission> GetAll(Func<Mission, bool> filter)
    {
        return baseRepository.GetAll(filter);
    }

    public IEnumerable<Mission> GetAll() => GetAll(_ => true);

    public Mission? GetById(long id)
    {
        return baseRepository.GetById(id);
    }

    public Mission? Update(Mission entity)
    {
        baseRepository.Update(entity);
        return GetById(entity.Id);
    }
}
