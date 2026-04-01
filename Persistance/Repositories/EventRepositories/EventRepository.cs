using Application.DTOs.EventDtos;
using Application.Interfaces.IEventRespository;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Repositories.EventRepositories;

public class EventRepository(TicketingAppDBContext _dbContext) : IEventRepository
{
    public async Task<bool> AddEventAsync(Event eventCreate)
    {
        await _dbContext.Events.AddAsync(eventCreate);

        var temp = new Event()
        {
            Title = eventCreate.Title,
            Description = eventCreate.Description,
            Date = eventCreate.Date,
            ImageUrl = eventCreate.ImageUrl,
            CategoryId = eventCreate.CategoryId,
            Venue = eventCreate.Venue
        };

        var result = await _dbContext.SaveChangesAsync();

        return result >= 1 ? true : false;
    }
    public async Task<bool> DeleteEventAsync(string title)
    {
        var existingEvent = await _dbContext.Events.FirstOrDefaultAsync(e => e.Title == title);

        if (existingEvent == null)
        {
            return false;
        }
        else
        {
            existingEvent.DeletedAt = DateTime.UtcNow;

            var result = await _dbContext.SaveChangesAsync();

            return result >= 1 ? true : false;
        }
    }
    public async Task<IReadOnlyList<Event>> GetAllEventAsync()
    {
        var eventList = await _dbContext.Events.Include(e => e.Category).Include(e => e.Venue).ToListAsync();

        return eventList;
    }
    public async Task<Event> GetEventAsync(string title)
    {
        var result = await _dbContext.Events.Where(v => v.DeletedAt == null).Include(e => e.Category).Include(e => e.Venue).Where(e => e.DeletedAt == null).FirstOrDefaultAsync(e => e.Title == title);

        return result!;
    }
    public async Task<bool> UpdateEventAsync(string title, EventDto eventDto)
    {
        var existingEvent = await _dbContext.Events.Where(e => e.DeletedAt == null).FirstOrDefaultAsync(e => e.Title == title);

        existingEvent!.Title = eventDto.title;
        existingEvent.Description = eventDto.description;
        existingEvent.Date = eventDto.date;

        var result = await _dbContext.SaveChangesAsync();

        return result >= 1 ? true : false;
    }
}