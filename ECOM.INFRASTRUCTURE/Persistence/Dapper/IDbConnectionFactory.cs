using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ECOM.INFRASTRUCTURE.Persistence.Dapper
{
    public interface IDbConnectionFactory
    {
        IDbConnection Create();
    }
}
