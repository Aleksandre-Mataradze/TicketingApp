using Domain.Base;

namespace Domain.Models;

public class Ticket : Base<Guid>
{
    public decimal Price { get; set; }
    public required string SeatNumber { get; set; } // Unique
    public bool IsActive { get; set; } = true;
    public int EventId { get; set; }
}
