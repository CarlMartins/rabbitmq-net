using MediatR;

namespace AirlineBookingSystem.Payments.Application.Commands;

public record ProcessPaymentsCommand(Guid BookingId, decimal Amount) : IRequest<Guid>;