using System.Data;
using AirlineBookingSystem.Flights.Core.Entities;
using AirlineBookingSystem.Flights.Core.Repositories;
using Dapper;

namespace AirlineBookingSystem.Flights.Infrastructure.Repositories;

public class FlightRepository : IFlightRepository
{
    private readonly IDbConnection _dbConnection;

    public FlightRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<Flight>> GetFlightsAsync()
    {
        const string query = "SELECT * FROM Flights";

        return await _dbConnection.QueryAsync<Flight>(query);
    }

    public async Task AddFlightAsync(Flight flight)
    {
        const string query = "INSERT INTO Flights(Id, FlightNumber, Origin, Destination, DepartureTime, ArrivalTime) " +
                             "VALUES (@Id, @FlightNumber, @Origin, @Destination, @DepartureTime, @ArrivalTime)";

        await _dbConnection.ExecuteAsync(query, flight);
    }

    public async Task DeleteFlightAsync(Guid id)
    {
        const string query = "DELETE FROM Flights WHERE Id = @Id";

        await _dbConnection.ExecuteAsync(query, new { Id = id });
    }
}