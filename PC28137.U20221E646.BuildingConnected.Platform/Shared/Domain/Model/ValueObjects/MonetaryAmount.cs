namespace PC212053.U20221E646.BuildingConnected.Platform.Shared.Domain.Model.ValueObjects;

/// <summary>
///     Amount of money expressed in a currency.
/// </summary>
/// <remarks>Marcelo Barrientos Quispe</remarks>
public class MonetaryAmount
{
    /// <summary>Required by Entity Framework Core.</summary>
    protected MonetaryAmount() => Currency = string.Empty;

    /// <summary>Creates a monetary amount.</summary>
    /// <param name="value">Amount of money; cannot be negative.</param>
    /// <param name="currency">Currency of the amount; cannot be null or blank.</param>
    /// <exception cref="ArgumentOutOfRangeException">When the value is negative.</exception>
    /// <exception cref="ArgumentNullException">When the currency is null or blank.</exception>
    public MonetaryAmount(decimal value, string currency)
    {
        if (value < decimal.Zero)
            throw new ArgumentOutOfRangeException(nameof(value),
                "The value of a monetary amount cannot be negative.");

        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentNullException(nameof(currency),
                "The currency of a monetary amount cannot be null or blank.");

        Value = value;
        Currency = currency;
    }

    public decimal Value { get; private set; }
    public string Currency { get; private set; }

    /// <summary>Multiplies the amount by a factor, keeping the same currency.</summary>
    /// <param name="factor">Factor to multiply the value by.</param>
    /// <returns>A new monetary amount in the same currency.</returns>
    public MonetaryAmount MultiplyBy(decimal factor) => new(Value * factor, Currency);
}