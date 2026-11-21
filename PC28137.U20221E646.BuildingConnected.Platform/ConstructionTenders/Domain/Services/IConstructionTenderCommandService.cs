using PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Domain.Model.Aggregates;
using PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Domain.Model.Commands;

namespace PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Domain.Services;

/// <summary>Handles the commands of the Construction Tenders bounded context.</summary>
/// <remarks>Marcelo Barrientos Quispe</remarks>
public interface IConstructionTenderCommandService
{
    Task<ConstructionTender?> Handle(CreateConstructionTenderCommand command,
        CancellationToken cancellationToken = default);
}