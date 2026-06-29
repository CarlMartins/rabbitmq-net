using System.Data;
using System.Reflection;
using AirlineBookingSystem.Bookings.Application.Consumers;
using AirlineBookingSystem.Bookings.Application.Handlers;
using AirlineBookingSystem.Bookings.Core.Repositories;
using AirlineBookingSystem.Bookings.Infrastructure.Repositories;
using AirlineBookingSystem.BuildingBlocks.Common;
using MassTransit;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var assemblies = new[]
{
    Assembly.GetExecutingAssembly(),
    typeof(CreateBookingHandler).Assembly,
    typeof(GetBookingHandler).Assembly
};

builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(assemblies));

builder.Services.AddScoped<IBookingRepository, BookingRepository>();

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<NotificationEventConsumer>();

    config.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host(builder.Configuration["EventBusSettings:Host"]);
            cfg.ReceiveEndpoint(EventBusConstant.NotificationSentQueue, c =>
            {
                c.ConfigureConsumer<NotificationEventConsumer>(context);
            });
        }
    );
});

builder.Services.AddScoped<IDbConnection>(_ =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Airline Booking System - Bookings API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
