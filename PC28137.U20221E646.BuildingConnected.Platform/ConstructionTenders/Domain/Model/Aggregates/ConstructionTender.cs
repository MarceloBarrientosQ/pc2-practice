using PC212053.U20221E646.BuildingConnected.Platform.Shared.Domain.Model.ValueObjects;
using PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Domain.Model.Commands;
using PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Domain.Model.ValueObjects;

namespace PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Domain.Model.Aggregates;

/// <summary>
///     Bidding process published so that contractors can compete for a construction project.
/// </summary>
/// <remarks>Marcelo Barrientos Quispe</remarks>
public partial class ConstructionTender
{
    /// <summary>Required by Entity Framework Core.</summary>
    protected ConstructionTender()
    {
        ConstructionTenderIdentifier = new ConstructionTenderIdentifier();
        Title = string.Empty;
    }

    /// <summary>Registers a construction tender from the command that requested it.</summary>
    /// <param name="command">Data submitted to register the tender.</param>
    /// <exception cref="ArgumentException">
    ///     When the title is blank, the contracting entity is not a valid identifier, the status is
    ///     unknown, or the tender is being registered as published or closed.
    /// </exception>
    public ConstructionTender(CreateConstructionTenderCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.Title))
            throw new ArgumentException("The title of a construction tender cannot be blank.", nameof(command));

        if (!Guid.TryParse(command.ContractingEntityId, out var contractingEntity))
            throw new ArgumentException("The contracting entity identifier is not a valid value.", nameof(command));

        ConstructionTenderIdentifier = new ConstructionTenderIdentifier();
        ContractingEntityId = new ContractingEntityId(contractingEntity);
        Period = new ValidityPeriod(command.PeriodStartDate, command.PeriodEndDate);
        EstimatedValue = new MonetaryAmount(command.EstimatedValue, command.EstimatedCurrency);
        Title = command.Title;
        Status = ParseStatus(command.Status);
    }

    public int Id { get; }
    public ConstructionTenderIdentifier ConstructionTenderIdentifier { get; private set; }
    public ContractingEntityId ContractingEntityId { get; private set; } = null!;
    public ValidityPeriod Period { get; private set; } = null!;
    public MonetaryAmount EstimatedValue { get; private set; } = null!;
    public EConstructionTenderStatus Status { get; private set; }
    public string Title { get; private set; }

    /// <summary>Reads the requested status, applying the rules that govern registration.</summary>
    /// <param name="status">Status submitted by the client, if any.</param>
    /// <returns>The status the tender starts in.</returns>
    private static EConstructionTenderStatus ParseStatus(string? status)
    {
        // A tender that was not given a status starts as a draft.
        if (string.IsNullOrWhiteSpace(status)) return EConstructionTenderStatus.Draft;

        if (!Enum.TryParse<EConstructionTenderStatus>(status, true, out var parsed))
            throw new ArgumentException($"'{status}' is not a known construction tender status.", nameof(status));

        // A tender is never born published or closed: it reaches those stages later.
        if (parsed is not EConstructionTenderStatus.Draft)
            throw new ArgumentException(
                "A construction tender cannot be registered as published or closed.", nameof(status));

        return parsed;
    }
}