using Microsoft.EntityFrameworkCore;

namespace IL2CarrerReviverModel.Data.Repositories;

/// <summary>
/// Base repository class with long as id type
/// </summary>
/// <typeparam name="T">The return type of the repository</typeparam>
internal abstract class BaseRepository<T> : IBaseRepository<T, long>
{
    /// <summary>
    /// The database context to use
    /// </summary>
    private readonly IDbContextFactory<IlTwoDatabaseContext> dbContextFactory;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="dbContextFactory">The context factory to use</param>
    protected BaseRepository(IDbContextFactory<IlTwoDatabaseContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    /// <summary>
    /// Get the database context
    /// </summary>
    /// <returns>A disposable database context</returns>
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
