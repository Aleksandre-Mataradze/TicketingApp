using Application.Common;
using Application.DTOs.EventDtos;
using Application.Features.EventFeatures;
using Microsoft.AspNetCore.Mvc;

namespace TicketingApp.Controllers.EventController;

[ApiController]
[Route("api/[controller]")]
public class EventController(EventFeatures _eventFeatures) : ControllerBase
{
    [HttpPost]
    public async Task<Result<bool>> AddEventAsync(EventCreateDto eventCreate)
    {
        var result = await _eventFeatures.AddEventAsync(eventCreate);

        return result;
    }
    [HttpGet("{title}")]
    public async Task<Result<EventDto>> GetEventAsync(string title)
    {
        var result = await _eventFeatures.GetEventAsync(title);
        return result;
    }
    [HttpGet]
    public async Task<Result<IReadOnlyList<EventDto>>> GetAllEventAsync()
    {
        var result = await _eventFeatures.GetAllEventAsync();
        return result;
    }
    [HttpPut]
    public async Task<Result<bool>> UpdateEventAsync(string title, EventDto eventDto)
    {
        var result = await _eventFeatures.UpdateEventAsync(title, eventDto);
        return result;
    }
    [HttpDelete]
    public async Task<Result<bool>> DeleteEventAsync(string title)
    {
        var result = await _eventFeatures.DeleteEventAsync(title);
        return result;
    }
}
