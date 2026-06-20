using ECOM.APPLICATION.Common;
using ECOM.APPLICATION.Interfaces.Data;
using ECOM.APPLICATION.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ECOM.INFRASTRUCTURE.Repositories
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public EFRepository(ApplicationDbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public async Task<TEntity?> GetByIdAsync<TId>(TId id) => await DbSet.FindAsync(id);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await DbSet.ToListAsync();

        public async Task AddAsync(TEntity entity) => await DbSet.AddAsync(entity);

        public void Update(TEntity entity) => DbSet.Update(entity);

        public void Delete(TEntity entity) => DbSet.Remove(entity);

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate) => await DbSet.AnyAsync(predicate);

        public async Task<PagingItems<TEntity>> PagedAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>>? predicate = null)
        {
            var query = DbSet.AsNoTracking();
            if (predicate != null) query = query.Where(predicate);

            int totalRecords = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagingItems<TEntity>(items, totalRecords, pageNumber, pageSize);
        }
    }
}
