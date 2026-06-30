using AirlineBookingSystem.BuildingBlocks.Contracts.EventBus.Messages;
using AirlineBookingSystem.Notifications.Application.Interfaces;
using AirlineBookingSystem.Notifications.Core.Entities;
using MassTransit;

namespace AirlineBookingSystem.Notifications.Application.Services;

public class NotificationService : INotificationService
{
    private readonly IPublishEndpoint _publishEndpoint;

    public NotificationService(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task SendNotificationAsync(Notification notification)
    {
        // Simulate sending a notification (via e-mail or sms)
        Console.WriteLine($"Notification sent to {notification.Recipient}: {notification.Message}");
        
        var notificationEvent = new NotificationEvent(notification.Recipient, notification.Message, notification.Type);
        
        await _publishEndpoint.Publish(notificationEvent);
    }
}