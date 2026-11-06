using FactureGenerator.Application.Common.Interfaces;
using MediatR;

namespace FactureGenerator.Application.Features.Authentication.Commands;

public sealed class LoginUserCommandHandler(IIdentityService identityService)
    : IRequestHandler<LoginUserCommand, LoginResult>
{
    public Task<LoginResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        return identityService.LoginAsync(request.Email, request.Password, request.RememberMe, cancellationToken);
    }
}
