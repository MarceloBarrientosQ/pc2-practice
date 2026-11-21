using PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Domain.Model.Aggregates;

namespace PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Domain.Repositories;

/// <summary>Storage contract for construction tenders.</summary>
/// <remarks>Marcelo Barrientos Quispe</remarks>
public interface IConstructionTenderRepository
{
    Task AddAsync(ConstructionTender tender, CancellationToken cancellationToken = default);

    /// <summary>Answers whether a tender already carries the given identifier.</summary>
    /// <param name="identifier">Identifier to look for.</param>
    /// <param name="cancellationToken">Token that cancels the query.</param>
    /// <returns>True when a tender with that identifier is already stored.</returns>
    Task<bool> ExistsByIdentifierAsync(Guid identifier, CancellationToken cancellationToken = default);
}