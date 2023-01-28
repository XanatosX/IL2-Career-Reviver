namespace IL2CareerModel.Data.Repositories;

/// <summary>
/// Repository interface
/// </summary>
/// <typeparam name="T">The return type for the repository</typeparam>
/// <typeparam name="K">The type of the id for the repository</typeparam>
internal interface IBaseRepository<T, K>
{
    /// <summary>
    /// Get all the entries from the repository
    /// </summary>
    /// <returns>All the repository entries</returns>
    public IEnumerable<T> GetAll();

    /// <summary>
    /// Get all the entries from the repository matching the filter
    /// </summary>
    /// <param name="filter">The filter an entity need to match to be valid</param>
    /// <returns>A filtered list with all repository entries</returns>
    public IEnumerable<T> GetAll(Func<T, bool> filter);

    /// <summary>
    /// Get a entity by it's id
    /// </summary>
    /// <param name="key">The id of the entity</param>
    /// <returns>The entity matching the key or null if nothing was found</returns>
    public T? GetById(K key);

    /// <summary>
    /// Add an entity to the repository
    /// </summary>
    /// <param name="entity">The entity to add</param>
    /// <returns>The newly added entity or null if something went wrong</returns>
    public T? Add(T entity);

    /// <summary>
    /// Add multiple entities to the repository
    /// </summary>
    /// <param name="entities">The entitites to add</param>
    /// <returns>The list with the added entitites</returns>
    public IEnumerable<T> BulkAdd(IEnumerable<T> entities);

    /// <summary>
    /// Delete a entity from the repository
    /// </summary>
    /// <param name="entity">The entity to delete</param>
    /// <returns>True if delete was successful</returns>
    public bool Delete(T entity);

    /// <summary>
    /// Delete an entity by it's id
    /// </summary>
    /// <param name="key">The key of the entity to delete</param>
    /// <returns>True if delete was successful</returns>
    public bool DeleteById(K key);

    /// <summary>
    /// Update a entity in the repository
    /// </summary>
    /// <param name="entity">The entity to update</param>
    public void Update(T entity);
}
