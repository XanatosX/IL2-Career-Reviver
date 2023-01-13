using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL2CarrerReviverModel.Data.Repositories;
internal abstract class BaseRepository<T> : IRepository<T, long>
{
    private readonly IDbContextFactory<IlTwoDatabaseContext> dbContextFactory;

    public BaseRepository(IDbContextFactory<IlTwoDatabaseContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    protected IlTwoDatabaseContext GetDatabaseContext()
    {
        return dbContextFactory.CreateDbContext();
    }

    public virtual T? Add(T entity)
    {
        if (entity is null)
        {
            return default(T);
        }
        using (var dbContext = GetDatabaseContext())
        {
            dbContext.Add(entity);
        }

        return entity;
    }

    public virtual IEnumerable<T> BulkAdd(IEnumerable<T> entities)
    {
        using (var dbContext = GetDatabaseContext())
        {
            dbContext.AddRange(entities);
        }

        return entities;
    }

    public abstract bool Delete(T entity);

    public abstract bool DeleteById(long key);
    public abstract IEnumerable<T> GetAll(Func<T, bool> filter);

    public virtual IEnumerable<T> GetAll() => GetAll(_ => true);

    public abstract T? GetById(long key);

    public virtual void Update(T entity)
    {
        if (entity is null)
        {
            return;
        }
        using (var dbContext = GetDatabaseContext())
        {
            dbContext.Update(entity);
        }
    }
}
