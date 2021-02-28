using Bilicra.ProductCatalog.Common.Entities;
using Bilicra.ProductCatalog.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilicra.ProductCatalog.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _dbContext;
        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async virtual Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public async virtual Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().Where(x => !x.IsDeleted).ToListAsync();
        }
        public async virtual Task<IEnumerable<T>> GetAllAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>()
                   .Where(x => !x.IsDeleted)
                   .Where(predicate)
                   .ToListAsync();
        }
        public async virtual Task<T> CreateAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public async virtual Task<T> DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            entity.IsDeleted = true;
            await UpdateAsync(entity);
            return entity;
        }
    }
}
