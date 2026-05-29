using ECOM.APPLICATION;
using ECOM.INFRASTRUCTURE;
using ECOM.INFRASTRUCTURE.Persistence.Dapper;
using ECOM.INFRASTRUCTURE.Persistence.EntityFramework;
using ECOM.SHARED.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// =========================
// SERVICES
// =========================

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

// Auto DI
builder.Services.AddAutoDI(
    typeof(ApplicationAssembly).Assembly,
    typeof(InfrastructureAssembly).Assembly
);

// Dapper
builder.Services.AddScoped<IDbConnectionFactory, SqlConnectionFactory>();

builder.Services.AddScoped<IDapperRepository, DapperRepository>();

// EF
builder.Services.AddDbContext<BaseDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Default"));
});

var app = builder.Build();

// =========================
// MIDDLEWARE
// =========================

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