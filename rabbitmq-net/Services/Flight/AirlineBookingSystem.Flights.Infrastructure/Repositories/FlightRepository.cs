using AirlineBookingSystem.Flights.Core.Entities;
using AirlineBookingSystem.Flights.Core.Repositories;
using AirlineBookingSystem.Flights.Infrastructure.Data;
using MongoDB.Driver;

namespace AirlineBookingSystem.Flights.Infrastructure.Repositories;

public class FlightRepository : IFlightRepository
{
    private readonly IFlightContext _dbContext;

    public FlightRepository(IFlightContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Flight>> GetFlightsAsync()
    {
        return await _dbContext.Flights.Find(_ => true).ToListAsync();
    }

    public async Task AddFlightAsync(Flight flight)
    {
        await _dbContext.Flights.InsertOneAsync(flight);
    }

    public async Task DeleteFlightAsync(Guid id)
    {
        await _dbContext.Flights.DeleteOneAsync(f => f.Id == id);
    }
}