using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL2CarrerReviverModel.Data.Repositories;
internal interface IBaseRepository<T, K>
{
    public IEnumerable<T> GetAll();

    public IEnumerable<T> GetAll(Func<T, bool> filter);

    public T? GetById(K key);

    public T? Add(T entity);

    public IEnumerable<T> BulkAdd(IEnumerable<T> entities);

    public bool Delete(T entity);

    public bool DeleteById(K key);

    public void Update(T entity);
}
