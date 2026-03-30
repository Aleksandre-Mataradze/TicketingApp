using Application.DTOs.EventDtos;
using Application.Features.EventFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TicketingApp.Controllers.EventController;

[ApiController]
[Route("api/[controller]")]
public class EventController(EventFeatures _eventFeatures) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<bool>> AddEventAsync(EventCreateDto eventCreate)
    {
        var result = await _eventFeatures.AddEventAsync(eventCreate);

        return Ok(result);
    }
    [HttpGet("{title}")]
    public async Task<ActionResult<EventDto>> GetEventAsync(string title)
    {
        var result = await _eventFeatures.GetEventAsync(title);
        return Ok(result);
    }
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<EventDto>>> GetAllEventAsync()
    {
        var result = await _eventFeatures.GetAllEventAsync();
        return Ok(result);
    }
    [HttpPut]
    public async Task<ActionResult<bool>> UpdateEventAsync(string title, EventDto eventDto)
    {
        var result = await _eventFeatures.UpdateEventAsync(title, eventDto);
        return Ok(result);
    }
    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteEventAsync(string title)
    {
        var result = await _eventFeatures.DeleteEventAsync(title);
        return Ok(result);
    }
}
