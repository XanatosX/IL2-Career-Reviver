using IL2CarrerReviverModel.Data.Repositories;
using IL2CarrerReviverModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL2CarrerReviverModel.Data.Gateways;
internal sealed class CareerGateway : ICareerGateway
{
    private readonly IBaseRepository<Career, long> repository;

    public CareerGateway(IBaseRepository<Career, long> repository)
    {
        this.repository = repository;
    }

    public Career? GetById(long id)
    {
        return repository.GetById(id);
    }

    public IEnumerable<Career> GetAll()
    {
        return repository.GetAll();
    }

    public void UpdateCareer(Career career)
    {
        repository.Update(career);
    }
}
