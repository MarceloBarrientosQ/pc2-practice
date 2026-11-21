namespace PC28137.U20221E646.BuildingConnected.Platform.Shared.Domain.Repositories;

/// <summary>Commits the changes of one business operation as a single transaction.</summary>
/// <remarks>Marcelo Barrientos Quispe</remarks>
public interface IUnitOfWork
{
    Task CompleteAsync(CancellationToken cancellationToken = default);
}