namespace MyFinance.Shared.Exceptions;

public abstract class AppException : Exception
{
    protected AppException(string code, string message, int statusCode, Exception? innerException = null)
        : base(message, innerException)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("O código do erro é obrigatório.", nameof(code));

        if (statusCode is < 400 or > 599)
            throw new ArgumentOutOfRangeException(nameof(statusCode), "O status HTTP deve estar entre 400 e 599.");

        Code = code;
        StatusCode = statusCode;
    }

    public string Code { get; }

    public int StatusCode { get; }
}
