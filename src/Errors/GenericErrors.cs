using System.Net;

namespace LoomKit.Esits.Errors;

public static class GenericErrors
{
    // consts
    public const string NullObject = nameof(NullObject);
    public const string NotFound = nameof(NotFound);
    public const string Conflict = nameof(Conflict);
    public const string Validation = nameof(Validation);
    public const string Generic = nameof(Generic);
    public const string Unauthorized = nameof(Unauthorized);
    public const string Forbidden = nameof(Forbidden);
    public const string UnsuccessfulHttpStatusCode = nameof(UnsuccessfulHttpStatusCode);
    public const string Unexpected = nameof(Unexpected);

    // factories
    public static Error NullObjectError() => new(NullObject, string.Empty, "Object is null.");
    public static Error NullObjectError(Type type) => new(NullObject, string.Empty, $"Object of type {type.Name} is null.");
    public static Error NullObjectError<TValue>() => new(NullObject, string.Empty, $"Object of type {typeof(TValue).Name} is null.");
    public static Error NullObjectError(string message) => new(NullObject, string.Empty, message);
    public static Error NotFoundError(string message) => new(NotFound, string.Empty, message);
    public static Error NotFoundError(Type entityType, string entityId) => new(NotFound, string.Empty, $"{entityType.Name} with id {entityId} not found.");
    public static Error ConflictError(string entityId) => new(Conflict, string.Empty, $"Entity with id {entityId} already exists.");
    public static Error ConflictError(Type entityType, string entityId) => new(Conflict, string.Empty, $"{entityType.Name} with id {entityId} already exists.");
    public static Error ValidationError() => new(Validation, string.Empty, "Input data is not valid.", new Dictionary<string, List<string>>());
    public static Error ValidationError(string subject, string validationError) => new(Validation, string.Empty, "Input data is not valid.", new Dictionary<string, List<string>>() { { subject, [validationError] } });
    public static Error ValidationError(string subject, string[] validationErrors) => new(Validation, string.Empty, "Input data is not valid.", new Dictionary<string, List<string>>() { { subject, validationErrors.ToList() } });
    public static Error ValidationError(IDictionary<string, List<string>> validationErrors) => new(Validation, string.Empty, "Input data is not valid.", validationErrors.ToDictionary());
    public static Error ValidationError(IDictionary<string, string[]> validationErrors) => new(Validation, string.Empty, "Input data is not valid.", validationErrors.ToDictionary(kv => kv.Key, kv => kv.Value.ToList()));
    public static Error GenericError(string message) => new(Generic, string.Empty, message);
    public static Error UnauthorizedError(string message) => new(Unauthorized, string.Empty, message);
    public static Error UnauthorizedFlowError(string? message = null) => new(Unauthorized, string.Empty, message ?? "Unauthorized");
    public static Error ForbiddenError(string? message = null) => new(Forbidden, string.Empty, message ?? "Forbidden");
    public static Error UnsuccessfulHttpStatusCodeError(HttpStatusCode statusCode) => new(UnsuccessfulHttpStatusCode, statusCode.ToString(), $"Unsuccessful http status code ({statusCode})");
    public static Error UnexpectedError(Exception exception) => new(Unexpected, "Exception", exception.Message, exception);
}
