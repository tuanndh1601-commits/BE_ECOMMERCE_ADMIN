using ECOM.APPLICATION.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ECOM.APPLICATION.Interfaces.Data
{
    public interface IDapperService
    {
        Task<int> ExecuteAsync(DapperRequest request, IDbTransaction? transaction = null);
        Task<T?> QueryFirstOrDefaultAsync<T>(DapperRequest request, IDbTransaction? transaction = null);
        Task<IEnumerable<T>> QueryAsync<T>(DapperRequest request, IDbTransaction? transaction = null);
    }
}
