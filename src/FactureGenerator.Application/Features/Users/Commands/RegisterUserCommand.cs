using MediatR;

namespace FactureGenerator.Application.Features.Users.Commands;

public record RegisterUserCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string CompanyName,
    string? CompanyAddress,
    string? CompanyNif,
    string? CompanyStat) : IRequest<ResultDto>;

public record ResultDto(bool Succeeded, string UserId, IReadOnlyCollection<string> Errors)
{
    public static ResultDto Success(string userId) => new(true, userId, []);

    public static ResultDto Failure(IEnumerable<string> errors) => new(false, string.Empty, [.. errors]);
}
