using CarWash.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Обязательные сервисы
builder.Services.AddControllers();           // ← для контроллеров
builder.Services.AddAuthorization();

// В Program.cs
builder.Services.AddApplicationServices();      // ← из CarWash.Application
builder.Services.AddInfrastructureServices(builder.Configuration); // ← из CarWash.Infrastructure


// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// База данных
builder.Services.AddDbContext<CarWashDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); // ← обязательно!

app.Run();