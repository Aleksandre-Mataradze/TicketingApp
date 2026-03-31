using Application.Interfaces.ITicketRepository;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Repositories.TicketRepository;

public class TicketRepostiroy(TicketingAppDBContext _dbContext) : ITicketRepository
{
    public async Task<bool> AddTicketAsync(Ticket ticket)
    {
        await _dbContext.Tickets.AddAsync(ticket);
        var result = await _dbContext.SaveChangesAsync();

        return result >= 1 ? true : false ;
    }
    public async Task<bool> DeactiveTicketAsync(Guid id)
    {
        var ticket = await _dbContext.Tickets.FirstOrDefaultAsync(t => t.Id == id);

        if (ticket == null)
        {
            return false;
        }
        else
        {
            ticket.IsActive = false;

            return true;
        }
    }
}