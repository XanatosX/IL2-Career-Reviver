namespace IL2CareerModel.Data.Gateways;

/// <summary>
/// Gateway to write data to the table of type T
/// </summary>
/// <typeparam name="T">The table type for this gateway</typeparam>
public interface IWriteGateway<T>
{
    /// <summary>
    /// Update a single data set on the table
    /// </summary>
    /// <param name="entity">The updated entity</param>
    /// <returns>The updated entity or null if something went wrong</returns>
    T? Update(T entity);

    /// <summary>
    /// Delete a entity from the table by it's id
    /// </summary>
    /// <param name="id">The id to delete from the table</param>
    /// <returns>True if deletion was successful</returns>
    bool DeleteById(long id);

    /// <summary>
    /// Delete a entity from the table
    /// </summary>
    /// <param name="entity">The entity to delete</param>
    /// <returns>True if deletion was successful</returns>
    bool Delete(T entity);
}
