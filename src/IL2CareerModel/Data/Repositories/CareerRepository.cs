using IL2CareerModel.Models;
using Microsoft.EntityFrameworkCore;

namespace IL2CareerModel.Data.Repositories;
internal class CareerRepository : BaseRepository<Career>
{
    public CareerRepository(IDbContextFactory<IlTwoDatabaseContext> dbContextFactory) : base(dbContextFactory) { }

    public override bool Delete(Career entity)
    {
        return DeleteById(entity.Id);
    }

    public override bool DeleteById(long key)
    {
        bool returnValue = false;
        using (var context = GetDatabaseContext())
        {
            context.Careers.Where(entry => entry.Id == key)
                           .ExecuteDelete();
            returnValue = context.Careers.Any(entry => entry.Id == key);
        }
        return returnValue;
    }

    public override IEnumerable<Career> GetAll(Func<Career, bool> filter)
    {
        IEnumerable<Career> returnSet = Enumerable.Empty<Career>();
        using (var context = GetDatabaseContext())
        {
            returnSet = context.Careers.Include(a => a.Player)
                                       .Include(a => a.Squadron)
                                       .Where(filter)
                                       .ToList();
        }
        return returnSet;
    }

    public override Career? GetById(long key)
    {
        Career? returnValue = null;
        using (var context = GetDatabaseContext())
        {
            returnValue = context.Careers.Where(career => career.Id == key)
                                         .Include(a => a.Player)
                                         .Include(a => a.Squadron)
                                         .First();
        }
        return returnValue;
    }
}
