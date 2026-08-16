namespace MyFinance.Shared.Results;

public sealed record Error(string Code, string Message)
{
    public static readonly Error None = new(string.Empty, string.Empty);

    public static Error Validation(string message) => new("Erro de Validação", message);

    public static Error NotFound(string message) => new("Não encontrado", message);

    public static Error Conflict(string message) => new("Conflito", message);

    public static Error Unauthorized(string message) => new("Não autorizado", message);

    public static Error Forbidden(string message) => new("Acesso negado", message);

    public static Error Unexpected(string message = "Ocorreu um erro inesperado.") =>
        new("unexpected_error", message);
}