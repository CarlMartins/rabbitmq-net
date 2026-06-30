using AirlineBookingSystem.Flights.Core.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace AirlineBookingSystem.Flights.Infrastructure.Data;

public class FlightContext : IFlightContext
{
    public IMongoCollection<Flight> Flights { get; }

    public FlightContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);
        var dataBase = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
        
        Flights = dataBase.GetCollection<Flight>("Flights");
    }
}