namespace Tachyonoid.SharedKernel.ValueObjects;

/// <summary>
/// Cross-context value object representing a monetary amount with currency.
/// ISO 4217 currency codes. Immutable.
/// </summary>
public sealed record Money
{
    public decimal Value { get; }
    public string CurrencyCode { get; } // ISO 4217, e.g., "ZAR"

    public Money(decimal value, string currencyCode)
    {
        if (value < 0) throw new ArgumentException("Money value cannot be negative", nameof(value));
        if (string.IsNullOrWhiteSpace(currencyCode)) throw new ArgumentException("Currency code required", nameof(currencyCode));

        Value = value;
        CurrencyCode = currencyCode.ToUpperInvariant();
    }

    public static Money Zero(string currencyCode) => new(0, currencyCode);
    public static Money ZAR(decimal value) => new(value, "ZAR");
    public static Money USD(decimal value) => new(value, "USD");
    public static Money EUR(decimal value) => new(value, "EUR");

    public Money Add(Money other)
    {
        if (other.CurrencyCode != CurrencyCode)
            throw new InvalidOperationException($"Cannot add {other.CurrencyCode} to {CurrencyCode}");
        return new Money(Value + other.Value, CurrencyCode);
    }

    public Money Subtract(Money other)
    {
        if (other.CurrencyCode != CurrencyCode)
            throw new InvalidOperationException($"Cannot subtract {other.CurrencyCode} from {CurrencyCode}");
        return new Money(Value - other.Value, CurrencyCode);
    }

    public Money Multiply(decimal factor) => new(Value * factor, CurrencyCode);
    public Money Divide(decimal divisor) => new(Value / divisor, CurrencyCode);

    public override string ToString() => $"{CurrencyCode} {Value:N2}";
}
