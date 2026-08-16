namespace MyFinance.Shared.Exceptions;

public sealed class ValidationException : AppException
{
    public ValidationException(string message, IReadOnlyDictionary<string, string[]>? errors = null)
        : base("validation_error", message, 400)
    {
        Errors = errors ?? new Dictionary<string, string[]>();
    }

    public IReadOnlyDictionary<string, string[]> Errors { get; }
}

public sealed class NotFoundException : AppException
{
    public NotFoundException(string message) : base("not_found", message, 404)
    {
    }
}

public sealed class ConflictException : AppException
{
    public ConflictException(string message) : base("conflict", message, 409)
    {
    }
}

public sealed class UnauthorizedException : AppException
{
    public UnauthorizedException(string message = "Usuário não autenticado.")
        : base("unauthorized", message, 401)
    {
    }
}

public sealed class ForbiddenException : AppException
{
    public ForbiddenException(string message = "Usuário sem permissão para executar esta operação.")
        : base("forbidden", message, 403)
    {
    }
}

public sealed class BusinessRuleException : AppException
{
    public BusinessRuleException(string message, string code = "business_rule_violation")
        : base(code, message, 422)
    {
    }
}
