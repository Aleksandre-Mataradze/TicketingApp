

namespace Application.DTOs.EventDtos;

public record EventCreateDto(string title, string description, DateTime date, string ImageUrl, string categoryName, string venueName);