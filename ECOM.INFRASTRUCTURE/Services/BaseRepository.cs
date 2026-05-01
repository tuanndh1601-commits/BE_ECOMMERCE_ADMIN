using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ECOM.INFRASTRUCTURE.Services
{
    public abstract class BaseRepository
    {
        private readonly IDbConnectionFactory _context;

        protected BaseRepository(IDbConnectionFactory context)
        {
            _context = context;
        }

        // Hàm bổ trợ để thực thi logic trong scope của một Connection
        // Đảm bảo Connection luôn được đóng sau khi thực hiện xong
        protected async Task<T> ExecuteWithConnection<T>(Func<IDbConnection, Task<T>> query)
        {
            using var connection = _context.CreateConnection();
            return await query(connection);
        }

        // Store query danh sách + Total (Trả về Tuple)
        protected async Task<(IEnumerable<T> Data, int Total)> QueryPagedAsync<T>(string storeName, object? parameters = null)
        {
            return await ExecuteWithConnection(async db =>
            {
                using var multi = await db.QueryMultipleAsync(storeName, parameters, commandType: CommandType.StoredProcedure);
                var data = await multi.ReadAsync<T>();
                var total = await multi.ReadFirstAsync<int>();
                return (data, total);
            });
        }

        // Store query List<T>
        protected async Task<IEnumerable<T>> QueryListAsync<T>(string storeName, object? parameters = null)
        {
            return await ExecuteWithConnection(async db => await db.QueryAsync<T>(storeName, parameters, commandType: CommandType.StoredProcedure));
        }

        // Store query FirstOrDefault
        protected async Task<T?> QueryFirstOrDefaultAsync<T>(string storeName, object? parameters = null)
        {
            return await ExecuteWithConnection(async db => await db.QueryFirstOrDefaultAsync<T>(storeName, parameters, commandType: CommandType.StoredProcedure));
        }

        // Execute (Insert, Update, Delete)
        protected async Task<int> ExecuteAsync(string storeName, object? parameters = null)
        {
            return await ExecuteWithConnection(async db => await db.ExecuteAsync(storeName, parameters, commandType: CommandType.StoredProcedure));
        }
    }
}
