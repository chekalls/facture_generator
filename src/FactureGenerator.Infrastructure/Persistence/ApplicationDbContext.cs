using FactureGenerator.Domain.Entities;
using FactureGenerator.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FactureGenerator.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Company> Companies { get; set; }
    public DbSet<UserCompany> UserCompanies { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Les noms sont mis en minuscules via UseLowerCaseNamingConvention() (voir DependencyInjection.cs).
        builder.Entity<ApplicationUser>().ToTable("users");
        builder.Entity<IdentityRole>().ToTable("roles");
        builder.Entity<IdentityUserRole<string>>().ToTable("user_roles");
        builder.Entity<IdentityUserClaim<string>>().ToTable("user_claims");
        builder.Entity<IdentityUserLogin<string>>().ToTable("user_logins");
        builder.Entity<IdentityUserToken<string>>().ToTable("user_tokens");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("role_claims");

        // Identity nomme explicitement ces index : la convention de nommage ne s'y applique pas.
        builder.Entity<ApplicationUser>().HasIndex(u => u.NormalizedUserName).HasDatabaseName("usernameindex");
        builder.Entity<ApplicationUser>().HasIndex(u => u.NormalizedEmail).HasDatabaseName("emailindex");
        builder.Entity<IdentityRole>().HasIndex(r => r.NormalizedName).HasDatabaseName("rolenameindex");

        // Le mapping de l'entite metier est porte par l'infrastructure, pas par le domaine.
        builder.Entity<Company>(entity =>
        {
            entity.ToTable("companies");
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).UseIdentityByDefaultColumn();
            entity.Property(c => c.Nif).HasColumnType("varchar(10)");
            entity.Property(c => c.Stat).HasColumnType("varchar(17)");
            entity.Property(c => c.Address).HasColumnType("varchar(500)");
            entity.Property(c => c.Designation).HasColumnType("varchar(255)");
        });

        builder.Entity<UserCompany>(entity =>
        {
            entity.ToTable("user_companies");
            entity.HasKey(uc => new { uc.UserId, uc.CompanyId });
            entity.Property(uc => uc.UserId).HasColumnType("text");
            entity.HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(uc => uc.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne<Company>()
                .WithMany()
                .HasForeignKey(uc => uc.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
