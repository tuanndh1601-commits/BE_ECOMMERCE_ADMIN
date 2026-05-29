using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ECOM.SHARED.Models
{
    public class MethodResult<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = "Success";
        public HttpStatusCode HttpCode { get; set; }  = HttpStatusCode.OK;
        public T? Result { get; set; }

        public static implicit operator MethodResult<T>(T result)
        {
            return new MethodResult<T>
            {
                IsSuccess = true,
                Result = result,
                HttpCode = HttpStatusCode.OK
            };
        }
    }
}
