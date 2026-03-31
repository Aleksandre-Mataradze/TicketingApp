using Domain.Base;

namespace Domain.Models;

public class User : Base<int>
{
    public string Name { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool Admin { get; set; } = false;
    public UserDetails UserDetails { get; set; } = new UserDetails();
    public List<Ticket> Tickets = new List<Ticket>();
}