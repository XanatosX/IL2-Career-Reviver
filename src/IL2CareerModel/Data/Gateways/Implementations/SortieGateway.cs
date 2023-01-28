using IL2CareerModel.Data.Repositories;
using IL2CareerModel.Models;

namespace IL2CareerModel.Data.Gateways.Implementations;

/// <summary>
/// The gateway implementation to change the sortie table on the database
/// </summary>
internal class SortieGateway : ISortieGateway
{
    /// <summary>
    /// The data repository to use
    /// </summary>
    private readonly IBaseRepository<Sortie, long> repository;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="baseRepository">The base repository to use for working with the data</param>
    public SortieGateway(IBaseRepository<Sortie, long> repository)
    {
        this.repository = repository;
    }

    public bool Delete(Sortie sortie)
    {
        return repository.Delete(sortie);
    }

    public bool DeleteById(long id)
    {
        return repository.DeleteById(id);
    }

    public IEnumerable<Sortie> GetAll(Func<Sortie, bool> filter)
    {
        return repository.GetAll(filter);
    }

    public IEnumerable<Sortie> GetAll() => GetAll(_ => true);

    public Sortie? GetById(long id)
    {
        return repository.GetById(id);
    }

    public Sortie? Update(Sortie entity)
    {
        repository.Update(entity);
        return GetById(entity.Id);
    }
}
