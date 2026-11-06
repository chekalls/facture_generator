using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FactureGenerator.Infrastructure.Persistence;

/// <summary>
/// Utilise par les outils `dotnet ef` (migrations, bundle) afin de ne pas dependre du projet Web.
/// </summary>
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    private const string DefaultConnectionString =
        "Host=localhost;Port=5432;Database=facture_generator;Username=postgres;Password=postgres";

    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var connectionString =
            Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection") ?? DefaultConnectionString;

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(connectionString)
            .UseLowerCaseNamingConvention()
            .Options;

        return new ApplicationDbContext(options);
    }
}
