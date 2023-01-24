﻿using Microsoft.EntityFrameworkCore;

namespace IL2CarrerReviverModel.Data.Repositories;
internal abstract class BaseRepository<T> : IBaseRepository<T, long>
{
    private readonly IDbContextFactory<IlTwoDatabaseContext> dbContextFactory;

    protected BaseRepository(IDbContextFactory<IlTwoDatabaseContext> dbContextFactory)
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
            return default;
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

    public virtual bool Delete(T entity)
    {
        bool status = false;
        if (entity is null)
        {
            return status;
        }
        using (var context = GetDatabaseContext())
        {
            context.Remove(entity);
            status = context.SaveChanges() > 0;
        }
        return status;
    }

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
            dbContext.SaveChanges();
        }
    }
}
