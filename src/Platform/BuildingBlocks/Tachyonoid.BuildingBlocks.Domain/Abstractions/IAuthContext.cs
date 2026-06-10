namespace Tachyonoid.BuildingBlocks.Domain;

/// <summary>
/// The platform's only view of identity. It is deliberately minimal and
/// provider-agnostic: the engine knows the current tenant, subject, and scopes,
/// but nothing about how they were authenticated. Any external identity and
/// access-management provider (Keycloak, Microsoft Entra ID, Auth0, or any
/// OpenID Connect server) is plugged in behind this abstraction at the host
/// layer, so swapping providers is a configuration change, never a code change.
/// </summary>
public interface IAuthContext
{
    /// <summary>The tenant the current request operates within. Every piece of metadata is tenant-scoped.</summary>
    string TenantId { get; }

    /// <summary>A stable identifier for the authenticated subject (a user or a partner system).</summary>
    string SubjectId { get; }

    /// <summary>The scopes or permissions granted to the current subject.</summary>
    IReadOnlySet<string> Scopes { get; }

    bool IsAuthenticated { get; }

    bool HasScope(string scope) => Scopes.Contains(scope);
}
