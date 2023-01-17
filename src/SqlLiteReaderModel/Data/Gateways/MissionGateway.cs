using IL2CarrerReviverModel.Data.Repositories;
using IL2CarrerReviverModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL2CarrerReviverModel.Data.Gateways;
internal class MissionGateway : IMissionGateway
{
    private readonly IBaseRepository<Mission, long> baseRepository;

    public MissionGateway(IBaseRepository<Mission, long> baseRepository)
    {
        this.baseRepository = baseRepository;
    }

    public bool DeleteById(long id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Mission> GetAll(Func<Mission, bool> filter)
    {
        return baseRepository.GetAll(filter);
    }

    public Mission? GetById(long id)
    {
        throw new NotImplementedException();
    }
}
