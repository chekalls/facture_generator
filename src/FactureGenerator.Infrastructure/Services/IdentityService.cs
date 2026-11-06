using FactureGenerator.Application.Common.Interfaces;
using FactureGenerator.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace FactureGenerator.Infrastructure.Services;

public sealed class IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    : IIdentityService
{
    public async Task<CreateUserResult> CreateUserAsync(
        string email,
        string password,
        string firstName,
        string lastName,
        CancellationToken cancellationToken = default)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FirstName = firstName,
            LastName = lastName
        };

        var result = await userManager.CreateAsync(user, password);

        return result.Succeeded
            ? CreateUserResult.Success(user.Id)
            : CreateUserResult.Failure(result.Errors.Select(error => error.Description).Distinct());
    }

    public async Task<LoginResult> LoginAsync(
        string email,
        string password,
        bool rememberMe,
        CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return LoginResult.Failure("Identifiants invalides.");
        }

        var result = await signInManager.PasswordSignInAsync(user, password, rememberMe, lockoutOnFailure: true);

        if (result.Succeeded)
        {
            return LoginResult.Success();
        }

        return result.IsLockedOut
            ? LoginResult.Failure("Le compte est temporairement verrouillé suite à trop de tentatives.")
            : LoginResult.Failure("Identifiants invalides.");
    }

    public async Task DeleteUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByIdAsync(userId);

        if (user is not null)
        {
            await userManager.DeleteAsync(user);
        }
    }
}
