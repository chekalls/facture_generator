using FactureGenerator.Domain.Entities;

namespace FactureGenerator.Application.Common.Interfaces;

/// <summary>
/// Abstraction sur la persistance des societes, implementee par l'infrastructure.
/// </summary>
public interface ICompanyService
{
    /// <summary>
    /// Enregistre la societe et la rattache a l'utilisateur donne dans une seule transaction.
    /// </summary>
    Task<int> CreateForUserAsync(Company company, string userId, CancellationToken cancellationToken = default);
}
