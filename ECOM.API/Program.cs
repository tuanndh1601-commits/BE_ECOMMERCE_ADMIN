using ECOM.APPLICATION;
using ECOM.INFRASTRUCTURE;
using ECOM.INFRASTRUCTURE.Persistence.Dapper;
using ECOM.INFRASTRUCTURE.Persistence.EntityFramework;
using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ECOM.SHARED.Services;

var builder = WebApplication.CreateBuilder(args);

// ======================
// 🔧 Add services
// ======================

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

// 👉 Auto-inject services
builder.Services.AddAutoDI(
    typeof(ApplicationAssembly).Assembly,
    typeof(InfrastructureAssembly).Assembly
);

builder.Services.AddScoped<IDbConnectionFactory, SqlConnectionFactory>();

builder.Services.AddScoped<IDapperRepository, DapperRepository>();


builder.Services.AddDbContext<BaseDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddOpenApi();
// ======================
// 🏗️ Build app
// ======================

var app = builder.Build();

// ======================
// 🌐 Middleware
// ======================

// 2. Kích hoạt Middleware (Phải nằm TRƯỚC app.Run)
if (app.Environment.IsDevelopment())
{

}

app.MapOpenApi();

app.MapScalarApiReference(options =>
{
    options
        .WithTitle("ECOM API")
        .WithTheme(ScalarTheme.DeepSpace)
        .WithDefaultHttpClient(
            ScalarTarget.CSharp,
            ScalarClient.HttpClient);
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();