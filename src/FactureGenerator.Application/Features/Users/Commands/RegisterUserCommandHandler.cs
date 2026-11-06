using FactureGenerator.Application.Common.Interfaces;
using FactureGenerator.Domain.Entities;
using MediatR;

namespace FactureGenerator.Application.Features.Users.Commands;

public sealed class RegisterUserCommandHandler(
    IIdentityService identityService,
    ICompanyService companyService) : IRequestHandler<RegisterUserCommand, ResultDto>
{
    public async Task<ResultDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = await identityService.CreateUserAsync(
            request.Email,
            request.Password,
            request.FirstName,
            request.LastName,
            cancellationToken);

        if (!user.Succeeded)
        {
            return ResultDto.Failure(user.Errors);
        }

        try
        {
            await companyService.CreateForUserAsync(
                new Company
                {
                    Designation = request.CompanyName,
                    Address = request.CompanyAddress ?? string.Empty,
                    Nif = request.CompanyNif ?? string.Empty,
                    Stat = request.CompanyStat ?? string.Empty
                },
                user.UserId!,
                cancellationToken);
        }
        catch
        {
            // Pas de transaction commune entre Identity et le metier : on compense la creation du compte.
            await identityService.DeleteUserAsync(user.UserId!, cancellationToken);
            throw;
        }

        return ResultDto.Success(user.UserId!);
    }
}
