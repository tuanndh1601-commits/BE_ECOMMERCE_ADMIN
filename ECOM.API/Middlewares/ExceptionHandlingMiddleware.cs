using ECOM.APPLICATION.Common;
using Microsoft.Data.SqlClient;
using System.Text.Json;

namespace ECOM.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = new MethodResult<object> { Success = false };

            switch (exception)
            {
                case FluentValidation.ValidationException valEx:
                    response.ErrorCode = "VALIDATION_ERROR";
                    response.Message = "Dữ liệu đầu vào không đúng định dạng kiểm thử.";
                    response.Data = valEx.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                    break;

                case SqlException sqlEx:
                    response.ErrorCode = $"DATABASE_ERROR_{sqlEx.Number}";
                    response.Message = "Hệ thống trục trặc kết nối dữ liệu Core.";
                    _logger.LogError(exception, "Database Exception Occured: {Msg}", sqlEx.Message);
                    break;

                default:
                    response.ErrorCode = "INTERNAL_SERVER_ERROR";
                    response.Message = "Lỗi hệ thống máy chủ tổng.";
                    _logger.LogCritical(exception, "Fatal Exception: {Msg}", exception.Message);
                    break;
            }

            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var resultJson = JsonSerializer.Serialize(response, jsonOptions);

            return context.Response.WriteAsync(resultJson);
        }
    }
}
