using IL2CarrerReviverModel.Models;
using Microsoft.EntityFrameworkCore;

namespace IL2CarrerReviverModel.Data.Repositories;
internal class MissionRepository : BaseRepository<Mission>
{
    public MissionRepository(IDbContextFactory<IlTwoDatabaseContext> dbContextFactory) : base(dbContextFactory)
    {
    }

    public override bool DeleteById(long key)
    {
        var mission = GetById(key);
        return mission is not null && Delete(mission);
    }

    public override IEnumerable<Mission> GetAll(Func<Mission, bool> filter)
    {
        IEnumerable<Mission> returnMissions = Enumerable.Empty<Mission>();
        using (var context = GetDatabaseContext())
        {
            returnMissions = context.Missions.Where(filter).ToList();
        }
        return returnMissions;
    }

    public override Mission? GetById(long key)
    {
        throw new NotImplementedException();
    }
}
