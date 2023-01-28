using IL2CareerModel.Data.Repositories;
using IL2CareerModel.Models;

namespace IL2CareerModel.Data.Gateways.Implementations;

/// <summary>
/// The gateway implementation to change the pilot table on the database
/// </summary>
internal class PilotGateway : IPilotGateway
{
    /// <summary>
    /// The data repository to use
    /// </summary>
    private readonly IBaseRepository<Pilot, long> repository;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="baseRepository">The base repository to use for working with the data</param>
    public PilotGateway(IBaseRepository<Pilot, long> repository)
    {
        this.repository = repository;
    }

    public bool Delete(Pilot entity)
    {
        return repository.Delete(entity);
    }

    public bool DeleteById(long id)
    {
        return repository.DeleteById(id);
    }

    public IEnumerable<Pilot> GetAll()
    {
        return repository.GetAll();
    }

    public IEnumerable<Pilot> GetAll(Func<Pilot, bool> filter)
    {
        return repository.GetAll(filter);
    }

    public Pilot? GetById(long id)
    {
        return repository.GetById(id);
    }

    public Pilot? Update(Pilot pilot)
    {
        repository.Update(pilot);
        return GetById(pilot.Id);
    }
}
