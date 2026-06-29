using AirlineBookingSystem.Notifications.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AirlineBookingSystem.Notifications.Api.Controllers;

[ApiController]
[Route("api/notifications")]
public class NotificationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> SendNotification([FromBody] SendNoticationCommand commmand)
    {
        await _mediator.Send(commmand);
        
        return Ok();
    }
}