using Domain.Base;

namespace Domain.Models;

public class User : Base<int>
{
    public required string  Name { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public UserDetails UserDetails { get; set; } = new UserDetails();
    public ICollection<Order> Orders = new List<Order>();
}