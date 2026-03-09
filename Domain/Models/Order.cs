using Domain.Base;
using Domain.Enums;

namespace Domain.Models;

public class Order : Base<Guid>
{
    public decimal TotalPrice { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public int UserId { get; set; }
}
