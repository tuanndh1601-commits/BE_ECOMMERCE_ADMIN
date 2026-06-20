using System;
using System.Collections.Generic;
using System.Text;

namespace ECOM.APPLICATION.Common
{
    public abstract class BaseRequest { }

    public class PagingRequest : BaseRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class FilterRequest
    {
        public string PropertyName { get; set; } = null!;
        public string Operator { get; set; } = "eq"; // eq, contains, gt, lt
        public string Value { get; set; } = null!;
    }

    public class SearchRequest : PagingRequest
    {
        public string? Keyword { get; set; }
        public List<FilterRequest> Filters { get; set; } = new();
    }
}
