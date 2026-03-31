namespace Application.DTOs.TicketDtos;

public record TicketCreateDTO(string seatNumber, decimal price, bool isActive, Guid eventId);
