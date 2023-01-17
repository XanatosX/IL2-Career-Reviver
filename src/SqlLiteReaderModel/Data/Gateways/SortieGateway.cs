using IL2CarrerReviverModel.Data.Repositories;
using IL2CarrerReviverModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL2CarrerReviverModel.Data.Gateways;
internal class SortieGateway : ISortieGateway
{
    private readonly IBaseRepository<Sortie, long> repository;

    public SortieGateway(IBaseRepository<Sortie, long> repository)
    {
        this.repository = repository;
    }

    public bool DeleteById(long id)
    {
        return repository.DeleteById(id);
    }

    public IEnumerable<Sortie> GetAll(Func<Sortie, bool> filter)
    {
        return repository.GetAll(filter);
    }

    public Sortie? GetById(long id)
    {
        return repository.GetById(id);
    }
}
