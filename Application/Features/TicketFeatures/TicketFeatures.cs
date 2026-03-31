using Application.DTOs.TicketDtos;
using Application.Interfaces.ITicketRepository;
using Domain.Models;

namespace Application.Features.TicketFeatures;

public class TicketFeatures(ITicketRepository _ticketRepository) 
{
    public async Task<bool> AddTicketAsync(TicketCreateDTO ticket, int userId)
    {
        if (ticket == null)
        {
            return false;
        }
        else
        {
            var tempTicket = new Ticket()
            {
                SeatNumber = ticket.seatNumber,
                Price = ticket.price,
                IsActive = true,
                EventId = ticket.eventId,
                UserId = userId
            };

            return await _ticketRepository.AddTicketAsync(tempTicket);
        }
    }
    public async Task<bool> DeactivateTicketAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            return false;
        }
        else
        {
            return await _ticketRepository.DeactiveTicketAsync(id);
        }
    }
}
