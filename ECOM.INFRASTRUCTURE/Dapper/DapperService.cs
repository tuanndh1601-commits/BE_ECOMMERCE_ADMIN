using Dapper;
using ECOM.APPLICATION.Common;
using ECOM.APPLICATION.Interfaces.Data;
using System.Data;

namespace ECOM.INFRASTRUCTURE.Dapper
{
    public class DapperService : IDapperService
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public DapperService(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> ExecuteAsync(DapperRequest request, IDbTransaction? transaction = null)
        {
            var connection = transaction?.Connection ?? _connectionFactory.CreateConnection();
            return await connection.ExecuteAsync(request.StoreName, request.Parameters, transaction, request.CommandTimeout, request.CommandType);
        }

        public async Task<T?> QueryFirstOrDefaultAsync<T>(DapperRequest request, IDbTransaction? transaction = null)
        {
            var connection = transaction?.Connection ?? _connectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<T>(request.StoreName, request.Parameters, transaction, request.CommandTimeout, request.CommandType);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(DapperRequest request, IDbTransaction? transaction = null)
        {
            var connection = transaction?.Connection ?? _connectionFactory.CreateConnection();
            return await connection.QueryAsync<T>(request.StoreName, request.Parameters, transaction, request.CommandTimeout, request.CommandType);
        }
    }
}
