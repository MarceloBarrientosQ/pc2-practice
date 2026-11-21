namespace PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Interfaces.REST.Resources;

/// <summary>Body accepted when registering a construction tender.</summary>
/// <remarks>Marcelo Barrientos Quispe</remarks>
public record CreateConstructionTenderResource(
    string ContractingEntityId,
    string PeriodStartDate,
    string PeriodEndDate,
    decimal EstimatedValue,
    string EstimatedCurrency,
    string Title,
    string? Status);