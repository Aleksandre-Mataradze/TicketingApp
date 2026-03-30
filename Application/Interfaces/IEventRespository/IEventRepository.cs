using Application.DTOs.EventDtos;
using Domain.Models;

namespace Application.Interfaces.IEventRespository;

public interface IEventRepository
{
    public Task<bool> AddEventAsync(Event eventCreate);
    public Task<Event> GetEventAsync(string title);
    public Task<IReadOnlyList<Event>> GetAllEventAsync();
    public Task<bool> UpdateEventAsync(string title, EventDto eventDto);
    public Task<bool> DeleteEventAsync(string title);
}