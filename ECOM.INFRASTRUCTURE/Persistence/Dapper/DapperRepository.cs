using Dapper;
using ECOM.SHARED.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ECOM.INFRASTRUCTURE.Persistence.Dapper
{
    public class DapperRepository : IDapperRepository, IScopeDependency
    {
        private readonly IDbConnectionFactory _factory;

        public DapperRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<List<T>> QueryListAsync<T>(string sql, object? param = null)
        {
            using var con = _factory.Create();

            var result = await con.QueryAsync<T>(sql, param);

            return result.ToList();
        }

        public async Task<T?> QueryObjectAsync<T>(string sql, object? param = null)
        {
            using var con = _factory.Create();

            return await con.QueryFirstOrDefaultAsync<T>(sql, param);
        }

        public async Task<int> ExecuteAsync(string sql, object? param = null)
        {
            using var con = _factory.Create();

            return await con.ExecuteAsync(sql, param);
        }

        public async Task<(List<T1>, int)> QueryMultipleAsync<T1>(string store, object? param = null)
        {
            using var con = _factory.Create();

            using var multi = await con.QueryMultipleAsync(store, param, commandType: CommandType.StoredProcedure);

            var data = (await multi.ReadAsync<T1>()).ToList();
            var total = await multi.ReadFirstAsync<int>();

            return (data, total);
        }
    }
}
