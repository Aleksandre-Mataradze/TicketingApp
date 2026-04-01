using Application.Common;
using Application.DTOs.TicketDtos;
using Application.Interfaces.ITicketRepository;
using Domain.Models;

namespace Application.Features.TicketFeatures;

public class TicketFeatures(ITicketRepository _ticketRepository) 
{
    public async Task<Result<bool>> AddTicketAsync(TicketCreateDTO ticket, int userId)
    {
        if (ticket == null)
        {
            return Result<bool>.Fail("An errored while purchasing a ticket,");
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

            var result = await _ticketRepository.AddTicketAsync(tempTicket);

            return Result<bool>.Ok(result);
        }
    }
    public async Task<Result<bool>> DeactivateTicketAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            return Result<bool>.Fail("An error occured while deactivating ticket.");
        }
        else
        {
            var result = await _ticketRepository.DeactiveTicketAsync(id);

            return Result<bool>.Ok(result);
        }
    }
}