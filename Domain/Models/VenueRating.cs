using Domain.Base;

namespace Domain.Models;

public class VenueRating : Base<Guid>
{
    public double Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public int VenueId { get; set; }
    public Venue? Venue { get; set; }
}