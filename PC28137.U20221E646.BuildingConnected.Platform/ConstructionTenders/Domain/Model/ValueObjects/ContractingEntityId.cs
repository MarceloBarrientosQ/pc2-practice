namespace PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Domain.Model.ValueObjects;

/// <summary>
///     Reference to the contracting entity that owns a tender. It originates in another
///     bounded context, so this context only keeps and validates its value.
/// </summary>
/// <remarks>Marcelo Barrientos Quispe</remarks>
public class ContractingEntityId
{
    /// <summary>Required by Entity Framework Core.</summary>
    protected ContractingEntityId() { }

    /// <summary>Creates a reference to a contracting entity.</summary>
    /// <param name="value">Identifier issued by the owning bounded context.</param>
    /// <exception cref="ArgumentException">When the identifier is empty.</exception>
    public ContractingEntityId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("The contracting entity identifier cannot be empty.", nameof(value));
        Value = value;
    }

    public Guid Value { get; private set; }

    public override string ToString() => Value.ToString();
}