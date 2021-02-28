using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bilicra.ProductCatalog.DataAccess.Repository
{
    public interface IRepository<T> 
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task<T> DeleteAsync(Guid id);
    }
}
