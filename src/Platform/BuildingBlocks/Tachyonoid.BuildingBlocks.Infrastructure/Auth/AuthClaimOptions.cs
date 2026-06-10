namespace Tachyonoid.BuildingBlocks.Infrastructure;

/// <summary>
/// Names of the claims that carry tenant, subject and scope. Defaults follow the
/// common OpenID Connect conventions but are overridable per deployment, because
/// different identity providers emit different claim names for the same concepts.
/// </summary>
public sealed class AuthClaimOptions
{
    public string TenantClaim { get; set; } = "tenant_id";
    public string SubjectClaim { get; set; } = "sub";
    public string ScopeClaim { get; set; } = "scope";
}
