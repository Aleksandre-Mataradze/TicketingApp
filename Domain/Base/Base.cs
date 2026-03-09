namespace Domain.Base;

public class Base<T>
{
    public T Id { get; set; } = default(T)!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public DateTime DeletedAt { get; set; }
}
