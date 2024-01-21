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

public class SuccessResult : Result
{
    public SuccessResult(params string[]? messages)
        : base(true, messages)
    {
    }

    public static implicit operator SuccessResult(string message)
        => new(message);

    public static implicit operator SuccessResult(string[] messages)
        => new(messages);
}

public class SuccessResult<T> : Result
{
    public SuccessResult(T? data, params string[]? messages)
        : base(true, messages)
    {
        Data = data;
    }

    public T? Data { get; }

    public static implicit operator SuccessResult<T>(T? data)
        => new(data, null);
}

public class FailureResult : Result
{
    public FailureResult(params string[]? messages)
        : base(false, messages)
    {
    }

    public static implicit operator FailureResult(string message)
        => new(message);

    public static implicit operator FailureResult(string[] messages)
        => new(messages);
}
