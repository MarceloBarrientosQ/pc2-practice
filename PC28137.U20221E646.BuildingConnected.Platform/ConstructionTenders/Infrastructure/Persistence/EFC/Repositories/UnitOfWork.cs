using PC28137.U20221E646.BuildingConnected.Platform.Shared.Domain.Repositories;
using PC28137.U20221E646.BuildingConnected.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Infrastructure.Persistence.EFC.Repositories;

/// <summary>Commits the changes tracked by the database context.</summary>
/// <remarks>Marcelo Barrientos Quispe</remarks>
public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task CompleteAsync(CancellationToken cancellationToken = default) =>
        await context.SaveChangesAsync(cancellationToken);
}