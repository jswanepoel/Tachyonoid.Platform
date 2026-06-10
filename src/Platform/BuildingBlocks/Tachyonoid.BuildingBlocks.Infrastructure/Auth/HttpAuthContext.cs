using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Tachyonoid.BuildingBlocks.Domain;

namespace Tachyonoid.BuildingBlocks.Infrastructure;

/// <summary>
/// The single concrete <see cref="IAuthContext"/>. It reads tenant, subject and
/// scopes from the validated claims principal on the current request. It is
/// deliberately ignorant of which identity provider issued those claims: any
/// OpenID Connect server (Keycloak, Microsoft Entra ID, Auth0) is configured at
/// the host with standard JWT bearer validation, and this type reads the result.
/// Swapping providers is a host configuration change; this code never changes.
/// </summary>
public sealed class HttpAuthContext(IHttpContextAccessor accessor, AuthClaimOptions options) : IAuthContext
{
    private ClaimsPrincipal? Principal => accessor.HttpContext?.User;

    public bool IsAuthenticated => Principal?.Identity?.IsAuthenticated ?? false;

    public string TenantId =>
        Principal?.FindFirstValue(options.TenantClaim) ?? string.Empty;

    public string SubjectId =>
        Principal?.FindFirstValue(options.SubjectClaim)
        ?? Principal?.FindFirstValue(ClaimTypes.NameIdentifier)
        ?? string.Empty;

    public IReadOnlySet<string> Scopes
    {
        get
        {
            var raw = Principal?.FindFirstValue(options.ScopeClaim);
            if (string.IsNullOrWhiteSpace(raw)) return new HashSet<string>();
            return raw.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                      .ToHashSet(StringComparer.Ordinal);
        }
    }
}
