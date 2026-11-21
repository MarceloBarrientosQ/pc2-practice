using PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Domain.Model.Aggregates;
using PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Interfaces.REST.Resources;

namespace PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Interfaces.REST.Transform;

/// <summary>Turns the stored aggregate into the resource the API returns.</summary>
/// <remarks>Marcelo Barrientos Quispe</remarks>
public static class ConstructionTenderResourceFromEntityAssembler
{
    /// <summary>Builds the resource returned for a stored tender.</summary>
    /// <param name="entity">Construction tender held by the platform.</param>
    /// <returns>The resource exposed by the API.</returns>
    public static ConstructionTenderResource ToResourceFromEntity(ConstructionTender entity) =>
        new(entity.Id,
            entity.ConstructionTenderIdentifier.Value.ToString(),
            entity.ContractingEntityId.Value.ToString(),
            entity.Period.StartDate.ToString("yyyy-MM-dd"),
            entity.Period.EndDate.ToString("yyyy-MM-dd"),
            entity.EstimatedValue.Value,
            entity.EstimatedValue.Currency,
            entity.Title);
}