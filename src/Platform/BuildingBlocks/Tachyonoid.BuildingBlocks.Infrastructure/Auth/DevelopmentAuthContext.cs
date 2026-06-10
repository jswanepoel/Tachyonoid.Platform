using Tachyonoid.BuildingBlocks.Domain;

namespace Tachyonoid.BuildingBlocks.Infrastructure;

/// <summary>
/// A development-only auth context used when no identity provider is configured,
/// so the services run out of the box for local exploration and tests. It reports
/// a fixed development tenant and broad scopes. It is never registered when an
/// authority is present, so production always uses the real claims-based context.
/// </summary>
public sealed class DevelopmentAuthContext : IAuthContext
{
    public const string DevelopmentTenant = "demo";

    public string TenantId => DevelopmentTenant;
    public string SubjectId => "dev-user";
    public IReadOnlySet<string> Scopes { get; } = new HashSet<string>(StringComparer.Ordinal) { "mappings:read", "mappings:write", "translate" };
    public bool IsAuthenticated => true;
}
