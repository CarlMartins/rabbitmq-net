using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using AirlineBookingSystem.Bookings.Core.Entities;
using AirlineBookingSystem.Bookings.Core.Repositories;
using Dapper;
using StackExchange.Redis;

namespace AirlineBookingSystem.Bookings.Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly IDatabase _redisDatabase;
    private const string RedisKeyPrefix = "booking_";

    public BookingRepository(IConnectionMultiplexer redisConnection)
    {
        _redisDatabase = redisConnection.GetDatabase();
    }

    public async Task<Booking?> GetBookingByIdAsync(Guid id)
    {
        var data = await _redisDatabase.StringGetAsync($"{RedisKeyPrefix}{id}");
        
        if (string.IsNullOrWhiteSpace(data))
            return null;
        
        return JsonSerializer.Deserialize<Booking>(data!);
    }

    public async Task AddBookingAsync(Booking booking)
    {
        var data = JsonSerializer.Serialize(booking);
        await _redisDatabase.StringSetAsync($"{RedisKeyPrefix}{booking.Id}", data);
    }
}