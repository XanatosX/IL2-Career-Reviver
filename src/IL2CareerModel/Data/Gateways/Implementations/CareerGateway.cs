using IL2CareerModel.Data.Repositories;
using IL2CareerModel.Models;

namespace IL2CareerModel.Data.Gateways.Implementations;

/// <summary>
/// The gateway implementation to change the career table on the database
/// </summary>
internal sealed class CareerGateway : ICareerGateway
{
    /// <summary>
    /// The data repository to use
    /// </summary>
    private readonly IBaseRepository<Career, long> repository;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="baseRepository">The base repository to use for working with the data</param>
    public CareerGateway(IBaseRepository<Career, long> repository)
    {
        this.repository = repository;
    }

    public Career? GetById(long id)
    {
        return repository.GetById(id);
    }

    public IEnumerable<Career> GetAll() => GetAll(_ => true);

    public Career? Update(Career career)
    {
        repository.Update(career);
        return GetById(career.Id);
    }

    public IEnumerable<Career> GetAll(Func<Career, bool> filter)
    {
        return repository.GetAll(filter);
    }

    public bool DeleteById(long id)
    {
        return repository.DeleteById(id);
    }

    public bool Delete(Career entity)
    {
        return repository.Delete(entity);
    }
}
