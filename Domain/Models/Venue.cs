using Domain.Base;

namespace Domain.Models;

public class Venue : Base<int>
{
    public required string Name { get; set; } = string.Empty;
    public required string Address { get; set; } = string.Empty;
    public int Capacity { get; set; }
}
