namespace Tachyonoid.SharedKernel.ValueObjects;

/// <summary>
/// Cross-context value object representing a phone number.
/// Normalized. Immutable.
/// </summary>
public sealed record PhoneNumber
{
    public string Value { get; }
    public string? CountryCode { get; } // e.g., "+27"

    public PhoneNumber(string value, string? countryCode = null)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Phone number required", nameof(value));

        // Strip non-numeric except leading +
        var normalized = value.Trim();
        if (normalized.StartsWith('+'))
        {
            CountryCode = normalized[..3]; // crude: assumes 2-digit country code
            Value = normalized[3..];
        }
        else
        {
            Value = normalized;
        }
    }

    public override string ToString() => $"{CountryCode}{Value}";
}
