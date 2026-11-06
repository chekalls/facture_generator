using FactureGenerator.Application.Common.Interfaces;
using FactureGenerator.Domain.Entities;
using FactureGenerator.Infrastructure.Persistence;

namespace FactureGenerator.Infrastructure.Services;

public sealed class CompanyService(ApplicationDbContext dbContext) : ICompanyService
{
    public async Task<int> CreateForUserAsync(Company company, string userId, CancellationToken cancellationToken = default)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        dbContext.Companies.Add(company);
        await dbContext.SaveChangesAsync(cancellationToken);

        dbContext.UserCompanies.Add(new UserCompany { UserId = userId, CompanyId = company.Id });
        await dbContext.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        return company.Id;
    }
}
