namespace AirlineBookingSystem.Flights.Core.Entities;

public class Flight
{
    public Guid Id { get; set; }
    public string FlightNumber { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public DateTime DepartureDate { get; set; }
    public DateTime ArrivalDate { get; set; }
}