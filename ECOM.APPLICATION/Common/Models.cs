using System;
using System.Collections.Generic;
using System.Text;

namespace ECOM.APPLICATION.Common
{
    public class MethodResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string ErrorCode { get; set; } = string.Empty;
        public T? Data { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;

        public static MethodResult<T> Ok(T data, string message = "Success") => new() { Success = true, Data = data, Message = message };
        public static MethodResult<T> Fail(string errorCode, string message) => new() { Success = false, ErrorCode = errorCode, Message = message };
    }
}
