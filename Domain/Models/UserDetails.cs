using Domain.Enums;

namespace Domain.Models;

public class UserDetails
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int Age { get; set; }
    public Gender? Gender { get; set; }
    public string profilePicture { get; set; } = string.Empty;
}
