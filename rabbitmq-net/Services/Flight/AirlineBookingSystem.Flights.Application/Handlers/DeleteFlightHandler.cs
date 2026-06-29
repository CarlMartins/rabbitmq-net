using AirlineBookingSystem.Flights.Application.Commands;
using AirlineBookingSystem.Flights.Core.Repositories;
using MediatR;

namespace AirlineBookingSystem.Flights.Application.Handlers;

public class DeleteFlightHandler : IRequestHandler<DeleteFlightCommand>
{
    private readonly IFlightRepository _flightRepository;

    public DeleteFlightHandler(IFlightRepository flightRepository)
    {
        _flightRepository = flightRepository;
    }

    public async Task Handle(DeleteFlightCommand request, CancellationToken cancellationToken)
    {
       await _flightRepository.DeleteFlightAsync(request.Id);
    }
}