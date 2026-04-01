namespace Application.Common;

public class Result<T>
{
    public bool Success { get; set; }
    public T Value { get; set; }
    public string Error { get; }

    private Result (bool success, T value, string error)
    {
        Success = success;
        Value = value;
        Error = error;
    }

    public static Result<T> Ok(T value) => new Result<T>(true, value, null!);
    public static Result<T> Fail(string error) => new Result<T>(false, default(T)!, error);
}