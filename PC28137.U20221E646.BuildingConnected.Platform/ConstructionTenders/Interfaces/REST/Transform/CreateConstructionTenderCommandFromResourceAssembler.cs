using System.Globalization;
using PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Domain.Model.Commands;
using PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Interfaces.REST.Resources;

namespace PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Interfaces.REST.Transform;

/// <summary>Turns the request body into the command the domain understands.</summary>
/// <remarks>Marcelo Barrientos Quispe</remarks>
public static class CreateConstructionTenderCommandFromResourceAssembler
{
    /// <summary>Builds the command from the submitted resource.</summary>
    /// <param name="resource">Body submitted by the client.</param>
    /// <returns>The command carrying the submitted data.</returns>
    /// <exception cref="ArgumentException">When a date is not written as yyyy-MM-dd.</exception>
    public static CreateConstructionTenderCommand ToCommandFromResource(
        CreateConstructionTenderResource resource) =>
        new(resource.ContractingEntityId,
            ParseDate(resource.PeriodStartDate, nameof(resource.PeriodStartDate)),
            ParseDate(resource.PeriodEndDate, nameof(resource.PeriodEndDate)),
            resource.EstimatedValue,
            resource.EstimatedCurrency,
            resource.Title,
            resource.Status);

    private static DateOnly ParseDate(string value, string field) =>
        DateOnly.TryParse(value, CultureInfo.InvariantCulture, out var date)
            ? date
            : throw new ArgumentException($"'{field}' must be a date written as yyyy-MM-dd.", field);
}