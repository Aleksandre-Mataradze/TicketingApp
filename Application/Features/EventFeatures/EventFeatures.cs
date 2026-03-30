using Application.DTOs.EventDtos;
using Application.Interfaces;
using Application.Interfaces.IEventRespository;
using Application.Interfaces.IVenueRepository;
using Domain.Models;

namespace Application.Features.EventFeatures;

public class EventFeatures(IEventRepository _eventRepository, ICategoryRepository _categoryRepository, IVenueRepository _venueRepository)
{
    public async Task<bool> AddEventAsync(EventCreateDto eventCreate)
    {
        if (eventCreate == null)
        {
            return false;
        }

        var category = await _categoryRepository.GetCategoryAsync(eventCreate.categoryName);
        var venue = await _venueRepository.GetVenueAsync(eventCreate.venueName);

        if (category == null || venue == null)
        {
            return false;
        }

        var temp = new Event()
        {
            Title = eventCreate.title,
            Description = eventCreate.description,
            Date = eventCreate.date,
            ImageUrl = eventCreate.ImageUrl,
            CategoryId = category.Id,
            Venue = venue
        };
        var result = await _eventRepository.AddEventAsync(temp);

        return result;
    }
    public async Task<EventDto> GetEventAsync(string title)
    {
        if (title == null || title == "")
        {
            return null!;
        }
        else
        {
            var result = await _eventRepository.GetEventAsync(title);

            return new EventDto(result.Title, result.Description, result.Date, result.ImageUrl ?? string.Empty, result.Venue.DeletedAt != null ? "" : result.Venue.Name, result.Category!.DeletedAt != null ? "" : result.Category.Name);
        }
    }
    public async Task<IReadOnlyList<EventDto>> GetAllEventAsync()
    {
        var events = await _eventRepository.GetAllEventAsync();
        if (events == null || events.Count == 0)
        {
            return new List<EventDto>();
        }
        else
        {

            var eventDtos = events.Select(e => new EventDto(e.Title, e.Description, e.Date, e.ImageUrl ?? string.Empty, e.Venue.DeletedAt != null ? "" : e.Venue.Name, e.Category!.DeletedAt != null ? "" : e.Category.Name)).ToList();
            return eventDtos;   
        }
    }
    public async Task<bool> UpdateEventAsync(string title, EventDto eventDto)
    {
        if (title == null || title == "" || eventDto == null)
        {
            return false;
        }
        else
        {
            var result = await _eventRepository.UpdateEventAsync(title, eventDto);
            return result;
        }
    }
    public async Task<bool> DeleteEventAsync(string title)
    {
        if (title == null || title == "")
        {
            return false;
        }
        else
        {
            var result = await _eventRepository.DeleteEventAsync(title);
            return result;
        }
    }
}
