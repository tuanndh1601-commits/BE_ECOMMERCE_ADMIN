using System;
using System.Collections.Generic;
using System.Text;

namespace ECOM.INFRASTRUCTURE.Persistence.Dapper
{
    public interface IDapperRepository
    {
        Task<List<T>> QueryListAsync<T>(
            string sql,
            object? param = null);

        Task<T?> QueryObjectAsync<T>(
            string sql,
            object? param = null);

        Task<int> ExecuteAsync(
            string sql,
            object? param = null);
    }
}
