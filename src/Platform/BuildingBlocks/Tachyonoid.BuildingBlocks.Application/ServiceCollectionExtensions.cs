using Microsoft.Extensions.DependencyInjection;

namespace Tachyonoid.BuildingBlocks.Application;

/// <summary>
/// Extension methods for registering the custom mediator and pipeline behaviors.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Register the custom mediator (ISender) and all pipeline behaviors.
    /// </summary>
    public static IServiceCollection AddHermesMediator(
        this IServiceCollection services)
    {
        services.AddSingleton<ISender, Sender>();

        // Register pipeline behaviors (order matters: they wrap in reverse registration order)
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

        return services;
    }
}
