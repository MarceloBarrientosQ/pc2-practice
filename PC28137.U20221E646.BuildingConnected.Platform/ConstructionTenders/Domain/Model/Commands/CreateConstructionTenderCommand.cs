namespace PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Domain.Model.Commands;

/// <summary>Request to register a construction tender.</summary>
/// <remarks>Marcelo Barrientos Quispe</remarks>
public record CreateConstructionTenderCommand(
    string ContractingEntityId,
    DateOnly PeriodStartDate,
    DateOnly PeriodEndDate,
    decimal EstimatedValue,
    string EstimatedCurrency,
    string Title,
    string? Status);