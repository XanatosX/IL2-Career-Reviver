using IL2CareerModel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL2CareerModel.Data.Repositories;
internal class PilotRepository : BaseRepository<Pilot>
{
    public PilotRepository(IDbContextFactory<IlTwoDatabaseContext> dbContextFactory) : base(dbContextFactory) { }

    public override bool Delete(Pilot entity)
    {
        throw new NotImplementedException();
    }

    public override bool DeleteById(long key)
    {
        throw new NotImplementedException();
    }

    public override IEnumerable<Pilot> GetAll(Func<Pilot, bool> filter)
    {

        IEnumerable<Pilot> returnPilots = Enumerable.Empty<Pilot>();
        using (var context = GetDatabaseContext())
        {
            returnPilots = context.Pilots.Include(p => p.Squadron)
                                         .Where(filter)
                                         .ToList();
        }
        return returnPilots;
    }

    public override Pilot? GetById(long key)
    {

        Pilot? returnPilot = null;
        using (var context = GetDatabaseContext())
        {
            returnPilot = context.Pilots.Where(pilot => pilot.Id == key).FirstOrDefault();
        }
        return returnPilot;
    }
}
