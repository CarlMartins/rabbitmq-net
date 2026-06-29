using System.Data;
using System.Reflection;
using AirlineBookingSystem.Notifications.Application.Handlers;
using AirlineBookingSystem.Notifications.Application.Interfaces;
using AirlineBookingSystem.Notifications.Application.Services;
using AirlineBookingSystem.Notifications.Core.Repositories;
using ClassLibrary1aAirlineBookingSystem.Notifications.Infrastructure.Repositories;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var assemblies = new[]
{
    Assembly.GetExecutingAssembly(),
    typeof(SendNoticationHandler).Assembly
};

builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(assemblies));

builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddScoped<IDbConnection>(_ =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Airline Booking System - Notifications API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
