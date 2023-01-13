using IL2CarrerReviverModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL2CarrerReviverModel.Data.Gateways;
public interface IPilotGateway
{
    IEnumerable<Pilot> GetAll(Func<Pilot, bool> filter);

    IEnumerable<Pilot> GetAll();

    Pilot? GetById(long id);
}
