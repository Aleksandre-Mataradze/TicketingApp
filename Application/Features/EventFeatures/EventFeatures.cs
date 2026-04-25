using Application.Common;
using Application.DTOs.EventDtos;
using Application.Interfaces;
using Application.Interfaces.IEventRespository;
using Application.Interfaces.IVenueRepository;
using Domain.Models;

namespace Application.Features.EventFeatures;

public class EventFeatures(IEventRepository _eventRepository, ICategoryRepository _categoryRepository, IVenueRepository _venueRepository)
{
    public async Task<Result<bool>> AddEventAsync(EventCreateDto eventCreate)
    {
        if (eventCreate == null)
        {
            return Result<bool>.Fail("Error occured when creating a new event.");
        }

        var category = await _categoryRepository.GetCategoryAsync(eventCreate.categoryName);
        var venue = await _venueRepository.GetVenueAsync(eventCreate.venueName);

        if (category == null || venue == null)
        {
            return Result<bool>.Fail("Error occured when creating a new event. Choose both category and venue.");
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

        return Result<bool>.Ok(result);
    }
    public async Task<Result<EventDto>> GetEventAsync(string title)
    {
        if (title == null || title == "")
        {
            return Result<EventDto>.Fail("Please input valid event title.");
        }
        else
        {
            var result = await _eventRepository.GetEventAsync(title);

            var tempEvent =  new EventDto(result.Title, result.Description, result.Price, result.Date, result.ImageUrl ?? string.Empty, result.Venue.DeletedAt != null ? "" : result.Venue.Name, result.Category!.DeletedAt != null ? "" : result.Category.Name);

            return Result<EventDto>.Ok(tempEvent);
        }
    }
    public async Task<Result<IReadOnlyList<EventDto>>> GetAllEventAsync()
    {
        var events = await _eventRepository.GetAllEventAsync();
        if (events == null || events.Count == 0)
        {
            return Result<IReadOnlyList<EventDto>>.Fail("An error occured while requesting event list.");
        }
        else
        {

            var eventDtos = events.Where(e => e.DeletedAt == null).Select(e => new EventDto(e.Title, e.Description, e.Price, e.Date, e.ImageUrl ?? string.Empty, e.Venue.DeletedAt != null ? "" : e.Venue.Name, e.Category!.DeletedAt != null ? "" : e.Category.Name)).ToList();
            return Result<IReadOnlyList<EventDto>>.Ok(eventDtos);   
        }
    }
    public async Task<Result<bool>> UpdateEventAsync(string title, EventDto eventDto)
    {
        if (title == null || title == "" || eventDto == null)
        {
            return Result<bool>.Fail("An error occured while updating the event. Please fill all fields correctly");
        }
        else
        {
            var result = await _eventRepository.UpdateEventAsync(title, eventDto);

            return Result<bool>.Ok(result);
        }
    }
    public async Task<Result<bool>> DeleteEventAsync(string title)
    {
        if (title == null || title == "")
        {
            return Result<bool>.Fail("An error occured while deleting event. Please fill field with valid title.");
        }
        else
        {
            var result = await _eventRepository.DeleteEventAsync(title);

            return Result<bool>.Ok(result);
        }
    }
    public async Task<Result<IReadOnlyList<EventDto>>> GetAllEventByCategoryAsync(string categoryName)
    {
        var eventList = await _eventRepository.GetAllEventAsync();

        if (eventList == null || eventList.Count == 0)
        {
            return Result<IReadOnlyList<EventDto>>.Fail("An error occured while requesting event list.");
        }
        else
        {
            var eventDtos = eventList.Where(e => e.Category!.Name == categoryName && e.DeletedAt == null).Select(e => new EventDto(e.Title, e.Description, e.Price, e.Date, e.ImageUrl ?? string.Empty, e.Venue.DeletedAt != null ? "" : e.Venue.Name, e.Category!.DeletedAt != null ? "" : e.Category.Name)).ToList();

            return Result<IReadOnlyList<EventDto>>.Ok(eventDtos);
        }
    }
}