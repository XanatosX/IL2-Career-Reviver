using src.SqlLiteReaderModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.SqlLiteReaderModel.Services
{
    public interface ICareerInformation
    {
        Pilot[] GetPilots();

        Pilot[] GetPilots(bool showIronman);

        bool RevivePilot(Pilot pilotToRevive);
    }
}