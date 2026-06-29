using System.Data;
using System.Reflection;
using AirlineBookingSystem.Payments.Application.Handlers;
using AirlineBookingSystem.Payments.Core.Repositories;
using AirlineBookingSystem.Payments.Infrastructure.Repositories;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var assemblies = new[]
{
    Assembly.GetExecutingAssembly(),
    typeof(ProcessPaymentHandler).Assembly,
    typeof(RefundPaymentHandler).Assembly
};

builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(assemblies));

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

builder.Services.AddScoped<IDbConnection>(_ =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Airline Booking System - Payments API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
