using System.Text.RegularExpressions;

namespace Tachyonoid.SharedKernel.ValueObjects;

/// <summary>
/// Cross-context value object representing an email address.
/// Normalized to lowercase. Immutable.
/// </summary>
public sealed record Email
{
    private static readonly Regex EmailRegex = new(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Email required", nameof(value));
        if (!EmailRegex.IsMatch(value)) throw new ArgumentException("Invalid email format", nameof(value));

        Value = value.ToLowerInvariant().Trim();
    }

    public override string ToString() => Value;
}
