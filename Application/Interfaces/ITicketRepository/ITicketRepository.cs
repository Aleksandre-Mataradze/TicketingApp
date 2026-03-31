using Domain.Models;

namespace Application.Interfaces.ITicketRepository;

public interface ITicketRepository
{
    public Task<bool> AddTicketAsync(Ticket ticket);
    public Task<bool> DeactiveTicketAsync(Guid id);
}