using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tachyonoid.BuildingBlocks.Domain;

namespace Tachyonoid.BuildingBlocks.Infrastructure;

/// <summary>
/// Wires the platform's pluggable identity model into a host. The host supplies
/// an OpenID Connect authority and audience through configuration; standard JWT
/// bearer validation does the rest. No identity provider is hard-coded, so a
/// deployment points this at Keycloak, Microsoft Entra ID, Auth0, or any other
/// OpenID Connect server purely through settings.
/// </summary>
public static class AuthenticationSetup
{
    public static IServiceCollection AddPluggableAuthentication(
        this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("Authentication");
        var authority = section["Authority"];
        var audience = section["Audience"];

        var claimOptions = new AuthClaimOptions();
        section.GetSection("Claims").Bind(claimOptions);
        services.AddSingleton(claimOptions);

        services.AddHttpContextAccessor();
        if (string.IsNullOrWhiteSpace(authority))
        {
            // No identity provider configured: development mode. A fixed development
            // tenant lets the services run end to end without an external IAM.
            services.AddScoped<IAuthContext, DevelopmentAuthContext>();
        }
        else
        {
            services.AddScoped<IAuthContext, HttpAuthContext>();
        }

        // When no authority is configured (local development or tests), registration
        // is skipped so the host still starts; endpoints simply see an unauthenticated
        // context. Production deployments always configure an authority.
        if (!string.IsNullOrWhiteSpace(authority))
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = authority;
                    options.Audience = audience;
                    options.TokenValidationParameters.ValidateAudience = !string.IsNullOrWhiteSpace(audience);
                    options.RequireHttpsMetadata = section.GetValue("RequireHttpsMetadata", true);
                });
            services.AddAuthorization();
        }

        return services;
    }
}
