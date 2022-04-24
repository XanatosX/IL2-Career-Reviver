using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.SqlLiteReaderModel.Model
{
    //SELECT career.id, career.playerId, career.personageId, career.ironMan, career.state, career.insDate, pilot.name, pilot.lastName, squadron.airfield FROM career INNER JOIN pilot ON career.playerId == pilot.id INNER JOIN squadron ON career.id == squadron.careerId where career.isDeleted = 0;
    public record Pilot(int PilotId, string personageId, bool IronMan, bool Alive, DateTime CreationDate, string FirstName, string LastName, string currentAirfield);
}