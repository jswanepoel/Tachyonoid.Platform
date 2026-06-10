namespace Tachyonoid.SharedKernel.ValueObjects;

/// <summary>
/// Cross-context value object representing a physical address.
/// Immutable.
/// </summary>
public sealed record Address
{
    public string Street { get; }
    public string? Suburb { get; }
    public string City { get; }
    public string? Province { get; }
    public string? PostalCode { get; }
    public string Country { get; } // ISO 3166-1 alpha-2

    public Address(
        string street,
        string city,
        string? suburb = null,
        string? province = null,
        string? postalCode = null,
        string country = "ZA")
    {
        if (string.IsNullOrWhiteSpace(street)) throw new ArgumentException("Street required", nameof(street));
        if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City required", nameof(city));

        Street = street.Trim();
        City = city.Trim();
        Suburb = suburb?.Trim();
        Province = province?.Trim();
        PostalCode = postalCode?.Trim();
        Country = country.ToUpperInvariant();
    }

    public string ToSingleLine() =>
        $"{Street}, {Suburb}{(Suburb is not null ? ", " : "")}{City}, {Province}{(Province is not null ? ", " : "")}{PostalCode}, {Country}";
}
