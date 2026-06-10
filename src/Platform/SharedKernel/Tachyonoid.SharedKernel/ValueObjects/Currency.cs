namespace Tachyonoid.SharedKernel.ValueObjects;

/// <summary>
/// ISO 4217 currency definition.
/// </summary>
public sealed record Currency
{
    public string Code { get; } // ISO 4217, e.g., "ZAR"
    public string Name { get; }
    public string Symbol { get; }
    public int DecimalPlaces { get; }

    public Currency(string code, string name, string symbol, int decimalPlaces)
    {
        if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("Currency code required", nameof(code));
        Code = code.ToUpperInvariant();
        Name = name;
        Symbol = symbol;
        DecimalPlaces = decimalPlaces;
    }

    public static readonly Currency ZAR = new("ZAR", "South African Rand", "R", 2);
    public static readonly Currency USD = new("USD", "US Dollar", "$", 2);
    public static readonly Currency EUR = new("EUR", "Euro", "€", 2);
    public static readonly Currency GBP = new("GBP", "British Pound", "£", 2);
}
