using IL2CarrerReviverModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL2CarrerReviverModel.Data.Gateways;
public interface ICareerGateway
{
    IEnumerable<Career> GetAll();

    Career? GetById(int id);
}
