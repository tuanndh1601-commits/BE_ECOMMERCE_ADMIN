using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ECOM.INFRASTRUCTURE.Persistence.Dapper
{
    public class SqlConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public SqlConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection Create()
        {
            return new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
