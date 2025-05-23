using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PRS.MonitoringService.Extensions;
using PRS.MonitoringService.Services;
using PRS.Shared.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<MonitoringDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null
            );
        })
);

builder.Services.AddScoped<IMonitoringService, MonitoringService>();

// SignalR for real-time monitoring
builder.Services.AddSignalR();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // app.ApplyMigrations();
}

// Wait for database to be ready
var maxRetries = 10;
var delay = TimeSpan.FromSeconds(5);

for (int i = 0; i < maxRetries; i++)
{
    try
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<MonitoringDbContext>();
        await context.Database.CanConnectAsync();
        break;
    }
    catch (Exception ex)
    {
        if (i == maxRetries - 1)
            throw;
            
        Console.WriteLine($"Database connection attempt {i + 1} failed: {ex.Message}");
        await Task.Delay(delay);
    }
}

// Then run migrations
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MonitoringDbContext>();
    context.Database.Migrate();
}

// app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();

app.MapControllers();

app.Run();