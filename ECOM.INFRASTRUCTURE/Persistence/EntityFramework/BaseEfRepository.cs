using ECOM.SHARED.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECOM.INFRASTRUCTURE.Persistence.EntityFramework
{
    public class BaseEfRepository<T> : IBaseEfRepository<T>, IScopeDependency where T : class
    {
        protected readonly BaseDbContext _context;

        public BaseEfRepository(BaseDbContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);

            return Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
