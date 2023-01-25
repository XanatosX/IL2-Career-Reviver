using IL2CarrerReviverModel.Models;
using Microsoft.EntityFrameworkCore;

namespace IL2CarrerReviverModel.Data.Repositories;
internal class SortieRepository : BaseRepository<Sortie>
{
    public SortieRepository(IDbContextFactory<IlTwoDatabaseContext> dbContextFactory) : base(dbContextFactory)
    {
    }

    public override bool DeleteById(long key)
    {
        var sortie = GetById(key);
        return sortie is not null && Delete(sortie);
    }

    public override IEnumerable<Sortie> GetAll(Func<Sortie, bool> filter)
    {
        IEnumerable<Sortie> resultSorties = Enumerable.Empty<Sortie>();
        using (var context = GetDatabaseContext())
        {
            resultSorties = context.Sorties.Where(filter).ToList();
        }
        return resultSorties;
    }

    public override Sortie? GetById(long key)
    {
        Sortie? returnSortie = null;
        using (var context = GetDatabaseContext())
        {
            returnSortie = context.Sorties.Where(sortie => sortie.Id == key)
                                          .FirstOrDefault();
        }
        return returnSortie;
    }
}
