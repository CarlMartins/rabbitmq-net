using System.Data;
using AirlineBookingSystem.Notifications.Core.Entities;
using AirlineBookingSystem.Notifications.Core.Repositories;
using Dapper;

namespace ClassLibrary1aAirlineBookingSystem.Notifications.Infrastructure.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly IDbConnection _dbConnection;

    public NotificationRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task LogNotificationAsync(Notification notification)
    {
        const string query = "INSERT INTO Notifications (Id, Recipient, Message, SentAt) " +
                             "VALUES (@Id, @Recipient, @Message, @SentAt)";
        
        await _dbConnection.ExecuteAsync(query, notification);
    }
}