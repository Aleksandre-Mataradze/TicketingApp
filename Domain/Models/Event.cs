using Domain.Base;

namespace Domain.Models;

public class Event : Base<Guid>
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public DateTime Date { get; set; }
    public string? ImageUrl { get; set; }
    public int CategoryId { get; set; }
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    public Venue Venue { get; set; } = new Venue();
}
