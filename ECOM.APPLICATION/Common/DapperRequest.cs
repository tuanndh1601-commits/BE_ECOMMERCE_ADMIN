using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ECOM.APPLICATION.Common
{
    public class DapperRequest
    {
        public string StoreName { get; set; } = null!;
        public object? Parameters { get; set; }
        public int? CommandTimeout { get; set; }
        public CommandType CommandType { get; set; } = CommandType.StoredProcedure;
    }
}
