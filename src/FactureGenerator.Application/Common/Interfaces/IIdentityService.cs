namespace FactureGenerator.Application.Common.Interfaces;

/// <summary>
/// Abstraction sur la gestion des comptes utilisateurs, implementee par l'infrastructure (ASP.NET Core Identity).
/// </summary>
public interface IIdentityService
{
    Task<CreateUserResult> CreateUserAsync(
        string email,
        string password,
        string firstName,
        string lastName,
        CancellationToken cancellationToken = default);

    Task<LoginResult> LoginAsync(
        string email,
        string password,
        bool rememberMe,
        CancellationToken cancellationToken = default);

    Task DeleteUserAsync(string userId, CancellationToken cancellationToken = default);
}

public sealed record CreateUserResult(bool Succeeded, string? UserId, IReadOnlyCollection<string> Errors)
{
    public static CreateUserResult Success(string userId) => new(true, userId, []);

    public static CreateUserResult Failure(IEnumerable<string> errors) => new(false, null, [.. errors]);
}

public sealed record LoginResult(bool Succeeded, IReadOnlyCollection<string> Errors)
{
    public static LoginResult Success() => new(true, []);

    public static LoginResult Failure(params string[] errors) => new(false, errors);
}
