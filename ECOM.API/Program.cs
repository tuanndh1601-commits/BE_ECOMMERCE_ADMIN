using ECOM.INFRASTRUCTURE.Services;

var builder = WebApplication.CreateBuilder(args);

// ======================
// 🔧 Add services
// ======================

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();           

// 👉 Auto-inject services
builder.Services.AddInfrastructure();

// 👉 DB Connection Factory
builder.Services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

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
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECOM API V1");
        c.RoutePrefix = "swagger"; // Đường dẫn truy cập sẽ là domain.com/swagger
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();