using Domain.Models;

namespace Application.DTOs.EventDtos;

public record EventDto(string title, string description, DateTime date, string ImageUrl, string venueName, string categoryName);
