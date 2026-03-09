using Domain.Base;

namespace Domain.Models;

public class Venue : Base<int>
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int Capacity { get; set; }
}
