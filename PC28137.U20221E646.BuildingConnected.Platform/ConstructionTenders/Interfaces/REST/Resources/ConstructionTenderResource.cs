namespace PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Interfaces.REST.Resources;

/// <summary>Construction tender as the API returns it.</summary>
/// <remarks>Marcelo Barrientos Quispe</remarks>
public record ConstructionTenderResource(
    int Id,
    string ConstructionTenderIdentifier,
    string ContractingEntityId,
    string PeriodStartDate,
    string PeriodEndDate,
    decimal EstimatedValue,
    string EstimatedCurrency,
    string Title);