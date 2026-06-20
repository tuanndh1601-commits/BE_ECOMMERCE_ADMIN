using ECOM.API.Middlewares;
using ECOM.APPLICATION.Interfaces.Data;
using ECOM.INFRASTRUCTURE.Data;
using ECOM.INFRASTRUCTURE.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// =========================
// SERVICES
// =========================

// 1. Connection String
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// 2. Đăng ký DbContext (EF Core)
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

// 3. Đăng ký DbConnectionFactory (Dapper) dưới dạng Singleton/Scoped tùy chọn kiến trúc
builder.Services.AddScoped<IDbConnectionFactory>(sp => new SqlConnectionFactory(connectionString));

// Mocking ICurrentUserService để hệ thống tự chạy được luôn (Tạm thời trả về System)
builder.Services.AddScoped<ICurrentUserService, MockCurrentUserService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ECOM API",
        Version = "v1",
        Description = "ECOM Backend API"
    });
});

// Tự động quét và Inject Service/Repository
builder.Services.AddAutoDependencies();

var app = builder.Build();

// =========================
// MIDDLEWARE
// =========================

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Swagger
app.UseSwagger();

app.UseSwaggerUI(options =>
{
    // Title trình duyệt
    options.DocumentTitle = "ECOM API";

    // Collapse endpoint
    options.DocExpansion(DocExpansion.None);

    // Ẩn schema/model
    options.DefaultModelsExpandDepth(-1);

    // Hiển thị thời gian request
    options.DisplayRequestDuration();

    // Search API
    options.EnableFilter();

    // KHÔNG set RoutePrefix
    // => dùng mặc định /swagger/index.html

    options.SwaggerEndpoint(
        "/swagger/v1/swagger.json",
        "ECOM API v1");
});

app.Run();

// Lớp tạm thời để điền Audit logs trước khi làm JWT hoàn chỉnh
public class MockCurrentUserService : ICurrentUserService
{
    public int Id => 0;
    public string Username => "master";
}