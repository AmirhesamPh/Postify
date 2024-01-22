namespace Postify.Responses;

public abstract class Result
{
    protected Result(bool isSuccess, string[]? messages)
    {
        IsSuccess = isSuccess;
        Messages = messages;
    }

    public bool IsSuccess { get; set; }

    public string[]? Messages { get; set; }
}

public class SuccessResult(params string[]? messages)
    : Result(true, messages)
{
    public static implicit operator SuccessResult(string message)
        => new(message);

    public static implicit operator SuccessResult(string[] messages)
        => new(messages);
}

public class SuccessResult<T>(T? data, params string[]? messages)
    : Result(true, messages)
{
    public T? Data { get; } = data;

    public static implicit operator SuccessResult<T>(T? data)
        => new(data, null);
}

public class FailureResult(params string[]? messages)
    : Result(false, messages)
{
    public static implicit operator FailureResult(string message)
        => new(message);

    public static implicit operator FailureResult(string[] messages)
        => new(messages);
}
