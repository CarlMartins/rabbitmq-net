using AirlineBookingSystem.Notifications.Application.Commands;
using AirlineBookingSystem.Notifications.Application.Interfaces;
using AirlineBookingSystem.Notifications.Core.Entities;
using MediatR;

namespace AirlineBookingSystem.Notifications.Application.Handlers;

public class SendNoticationHandler : IRequestHandler<SendNoticationCommand>
{
    private readonly INotificationService _notificationService;

    public SendNoticationHandler(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public async Task Handle(SendNoticationCommand request, CancellationToken cancellationToken)
    {
        var notification = new Notification
        {
            Id = Guid.NewGuid(),
            Recipient = request.Recipient,
            Message = request.Message,
            Type = request.Type
        };
        
        await _notificationService.SendNotificationAsync(notification);
    }
}