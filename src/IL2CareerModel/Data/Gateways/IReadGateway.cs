namespace IL2CareerModel.Data.Gateways;

/// <summary>
/// Gateway to recieve data from the table of type T
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IReadGateway<T>
{
    /// <summary>
    /// Get all the data sets in the table
    /// </summary>
    /// <returns>A list with all the data sets on the table of type T</returns>
    IEnumerable<T> GetAll();

    /// <summary>
    /// Get all the data sets in the table filtered by the provided filter
    /// </summary>
    /// <param name="filter">The filter to use on for the data set</param>
    /// <returns>A list with all the data filtered by the provided filter</returns>
    IEnumerable<T> GetAll(Func<T, bool> filter);

    /// <summary>
    /// Get an entity by it's id
    /// </summary>
    /// <param name="id">The id of the entity to get</param>
    /// <returns>The entity if found something or null</returns>
    T? GetById(long id);
}
