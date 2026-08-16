namespace MyFinance.Shared.Exceptions;

public sealed record ExceptionDetails(
    int StatusCode,
    string Code,
    string Message,
    IReadOnlyDictionary<string, string[]>? Errors = null,
    string? TraceId = null)
{
    public static ExceptionDetails FromException(Exception exception, string? traceId = null)
    {
        ArgumentNullException.ThrowIfNull(exception);

        return exception switch
        {
            ValidationException validationException => new(validationException.StatusCode,
                                                           validationException.Code,
                                                           validationException.Message,
                                                           validationException.Errors,
                                                           traceId),
            AppException appException => new(appException.StatusCode,
                                             appException.Code,
                                             appException.Message,
                                             TraceId: traceId),
                                             _ => new(
                                             500,
                                             "unexpected_error",
                                             "Ocorreu um erro inesperado.",
                                             TraceId: traceId)
        };
    }
}