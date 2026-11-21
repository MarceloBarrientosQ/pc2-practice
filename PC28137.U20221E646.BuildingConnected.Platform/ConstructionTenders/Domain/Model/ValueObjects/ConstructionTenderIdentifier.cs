namespace PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Domain.Model.ValueObjects;

/// <summary>
///     Globally unique identifier of a construction tender, generated when it is created.
/// </summary>
/// <remarks>Marcelo Barrientos Quispe</remarks>
public class ConstructionTenderIdentifier
{
    /// <summary>Generates a new identifier.</summary>
    public ConstructionTenderIdentifier() => Value = Guid.NewGuid();

    /// <summary>Rebuilds an identifier from a stored value.</summary>
    /// <param name="value">Value previously generated for a construction tender.</param>
    public ConstructionTenderIdentifier(Guid value) => Value = value;

    public Guid Value { get; private set; }

    public override string ToString() => Value.ToString();
}