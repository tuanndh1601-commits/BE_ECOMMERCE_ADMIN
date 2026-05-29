using System;
using System.Collections.Generic;
using System.Text;

namespace ECOM.INFRASTRUCTURE.Persistence.EntityFramework
{
    public interface IBaseEfRepository<T> where T : class
    {
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveAsync();
    }
}
