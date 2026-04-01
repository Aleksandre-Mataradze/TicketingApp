using Application.Common;
using Application.DTOs.TicketDtos;
using Application.Features.TicketFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TicketingApp.Controllers.TicketController;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TicketController(TicketFeatures _ticketFeatures) : ControllerBase
{
    [HttpPost]
    public async Task<Result<bool>> AddTicketsAsync(TicketCreateDTO ticket)
    {
        var userId = User.FindFirst("UserId")?.Value;

        var result = await _ticketFeatures.AddTicketAsync(ticket, int.Parse(userId!));
        
        return result;
    }
    [HttpPut]
    public async Task<Result<bool>> DeactivateTicketAsync(Guid id)
    {
        var result = await _ticketFeatures.DeactivateTicketAsync(id);

        return result;
    }
}