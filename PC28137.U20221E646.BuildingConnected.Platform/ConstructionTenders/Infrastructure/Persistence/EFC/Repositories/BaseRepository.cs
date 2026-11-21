using Microsoft.EntityFrameworkCore;
using PC28137.U20221E646.BuildingConnected.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Infrastructure.Persistence.EFC.Repositories;

/// <summary>Storage operations shared by every aggregate repository.</summary>
/// <typeparam name="TEntity">Aggregate root the repository stores.</typeparam>
/// <remarks>Marcelo Barrientos Quispe</remarks>
public abstract class BaseRepository<TEntity>(AppDbContext context) where TEntity : class
{
    protected readonly AppDbContext Context = context;

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default) =>
        await Context.Set<TEntity>().AddAsync(entity, cancellationToken);

    public async Task<IEnumerable<TEntity>> ListAsync(CancellationToken cancellationToken = default) =>
        await Context.Set<TEntity>().ToListAsync(cancellationToken);
}