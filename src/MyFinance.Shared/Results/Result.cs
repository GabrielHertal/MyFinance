namespace MyFinance.Shared.Results;

public class Result
{
    private protected Result(bool isSuccess, IReadOnlyCollection<Error> errors)
    {
        if (isSuccess && errors.Count > 0)
            throw new ArgumentException("Um resultado bem-sucedido não pode conter erros.", nameof(errors));

        if (!isSuccess && errors.Count == 0)
            throw new ArgumentException("Um resultado com falha deve conter ao menos um erro.", nameof(errors));

        IsSuccess = isSuccess;
        Errors = errors;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public IReadOnlyCollection<Error> Errors { get; }

    public Error Error => Errors.FirstOrDefault() ?? Error.None;

    public static Result Success() => new(true, Array.Empty<Error>());

    public static Result Failure(Error error)
    {
        ArgumentNullException.ThrowIfNull(error);
        return new Result(false, [error]);
    }

    public static Result Failure(IEnumerable<Error> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);
        return new Result(false, NormalizeErrors(errors));
    }

    public static Result<T> Success<T>(T value) => Result<T>.Success(value);

    public static Result<T> Failure<T>(Error error) => Result<T>.Failure(error);

    private protected static IReadOnlyCollection<Error> NormalizeErrors(IEnumerable<Error> errors)
    {
        var normalizedErrors = errors
            .Where(error => error is not null)
            .Distinct()
            .ToArray();

        if (normalizedErrors.Length == 0)
            throw new ArgumentException("Informe ao menos um erro.", nameof(errors));

        return normalizedErrors;
    }
}

public sealed class Result<T> : Result
{
    private readonly T? _value;

    private Result(T value) : base(true, Array.Empty<Error>())
    {
        _value = value;
    }

    private Result(IReadOnlyCollection<Error> errors) : base(false, errors)
    {
    }

    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Não é possível acessar o valor de um resultado com falha.");

    public static Result<T> Success(T value) => new(value);

    public new static Result<T> Failure(Error error)
    {
        ArgumentNullException.ThrowIfNull(error);
        return new Result<T>([error]);
    }

    public new static Result<T> Failure(IEnumerable<Error> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);
        return new Result<T>(NormalizeErrors(errors));
    }

    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<IReadOnlyCollection<Error>, TResult> onFailure)
    {
        ArgumentNullException.ThrowIfNull(onSuccess);
        ArgumentNullException.ThrowIfNull(onFailure);

        return IsSuccess ? onSuccess(Value) : onFailure(Errors);
    }

    public static implicit operator Result<T>(T value) => Success(value);

    public static implicit operator Result<T>(Error error) => Failure(error);
}
