using FactureGenerator.Application.Common.Interfaces;
using FactureGenerator.Infrastructure.Identity;
using FactureGenerator.Infrastructure.Persistence;
using FactureGenerator.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FactureGenerator.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString)
                   .UseLowerCaseNamingConvention());

        services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.User.RequireUniqueEmail = true;
        })
        .AddErrorDescriber<FrenchIdentityErrorDescriber>()
        .AddRoles<IdentityRole>()
        .AddSignInManager()
        .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddAuthentication(IdentityConstants.ApplicationScheme).AddIdentityCookies();

        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<ICompanyService, CompanyService>();

        return services;
    }
}
