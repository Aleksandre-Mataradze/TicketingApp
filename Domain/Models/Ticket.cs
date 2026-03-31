using Domain.Base;

namespace Domain.Models;

public class Ticket : Base<Guid>
{

    public decimal Price { get; set; }
    public required string SeatNumber { get; set; } // Unique
    public bool IsActive { get; set; } = true;
    public Guid EventId { get; set; }
    public Event Event { get; set; }
    public int UserId { get; set; }
}