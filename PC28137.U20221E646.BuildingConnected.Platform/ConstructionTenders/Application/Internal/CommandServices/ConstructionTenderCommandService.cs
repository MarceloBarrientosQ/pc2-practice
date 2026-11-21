using PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Domain.Model.Aggregates;
using PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Domain.Model.Commands;
using PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Domain.Repositories;
using PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Domain.Services;
using PC28137.U20221E646.BuildingConnected.Platform.Shared.Domain.Repositories;

namespace PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Application.Internal.CommandServices;

/// <summary>Registers construction tenders, enforcing the rules of the context.</summary>
/// <remarks>Marcelo Barrientos Quispe</remarks>
public class ConstructionTenderCommandService(
    IConstructionTenderRepository tenderRepository,
    IUnitOfWork unitOfWork) : IConstructionTenderCommandService
{
    /// <summary>Registers a construction tender when its identifier is not already taken.</summary>
    /// <param name="command">Data submitted to register the tender.</param>
    /// <param name="cancellationToken">Token that cancels the operation.</param>
    /// <returns>The stored tender, or null when its identifier is already in use.</returns>
    public async Task<ConstructionTender?> Handle(CreateConstructionTenderCommand command,
        CancellationToken cancellationToken = default)
    {
        // The aggregate validates every value object and the status it is allowed to start in.
        var tender = new ConstructionTender(command);

        // Two construction tenders never share an identifier.
        if (await tenderRepository.ExistsByIdentifierAsync(
                tender.ConstructionTenderIdentifier.Value, cancellationToken))
            return null;

        tender.CreatedAt = DateTimeOffset.UtcNow;
        tender.UpdatedAt = tender.CreatedAt;

        await tenderRepository.AddAsync(tender, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return tender;
    }
}