using Domain.Base;

namespace Domain.Models;

public class Category : Base<int>
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public string? IconUrl { get; set; }
    public ICollection<Event> Events { get; set; } = new List<Event>();
}
