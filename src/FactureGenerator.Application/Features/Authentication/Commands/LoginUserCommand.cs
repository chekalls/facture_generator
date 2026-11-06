using FactureGenerator.Application.Common.Interfaces;
using MediatR;

namespace FactureGenerator.Application.Features.Authentication.Commands;

public record LoginUserCommand(string Email, string Password, bool RememberMe) : IRequest<LoginResult>;
