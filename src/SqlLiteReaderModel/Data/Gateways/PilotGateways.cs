using IL2CarrerReviverModel.Data.Repositories;
using IL2CarrerReviverModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL2CarrerReviverModel.Data.Gateways;
internal class PilotGateway : IPilotGateway
{
    private readonly IRepository<Pilot, long> repository;

    public PilotGateway(IRepository<Pilot, long> repository)
    {
        this.repository = repository;
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
}
