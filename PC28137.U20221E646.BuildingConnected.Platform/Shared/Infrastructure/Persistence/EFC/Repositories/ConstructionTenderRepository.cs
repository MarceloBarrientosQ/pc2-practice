using Microsoft.EntityFrameworkCore;
using PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Domain.Model.Aggregates;
using PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Domain.Repositories;
using PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Infrastructure.Persistence.EFC.Repositories;
using PC28137.U20221E646.BuildingConnected.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace PC28137.U20221E646.BuildingConnected.Platform.Shared.Infrastructure.Persistence.EFC.Repositories;

/// <summary>Stores construction tenders with Entity Framework Core.</summary>
/// <remarks>Marcelo Barrientos Quispe</remarks>
public class ConstructionTenderRepository(AppDbContext context)
    : BaseRepository<ConstructionTender>(context), IConstructionTenderRepository
{
    /// <inheritdoc />
    public async Task<bool> ExistsByIdentifierAsync(Guid identifier,
        CancellationToken cancellationToken = default) =>
        await Context.Set<ConstructionTender>()
            .AnyAsync(t => t.ConstructionTenderIdentifier.Value == identifier, cancellationToken);
}