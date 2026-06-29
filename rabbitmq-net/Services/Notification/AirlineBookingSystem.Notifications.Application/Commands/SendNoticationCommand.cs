using MediatR;

namespace AirlineBookingSystem.Notifications.Application.Commands;

public record SendNoticationCommand(string Recipient, string Message, string Type) : IRequest;