using Microsoft.Extensions.DependencyInjection;

namespace Tachyonoid.BuildingBlocks.Application;

/// <summary>
/// Default implementation of ISender.
/// Resolves handlers from DI and executes the full pipeline.
/// </summary>
public sealed class Sender : ISender
{
    private readonly IServiceProvider _serviceProvider;

    public Sender(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async ValueTask<TResponse> Send<TResponse>(
        IRequest<TResponse> request, CancellationToken ct = default)
    {
        var requestType = request.GetType();
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));
        var handler = _serviceProvider.GetRequiredService(handlerType);

        // Get all pipeline behaviors for this request type
        var behaviorType = typeof(IPipelineBehavior<,>).MakeGenericType(requestType, typeof(TResponse));
        var behaviors = _serviceProvider.GetServices(behaviorType)
            .Cast<dynamic>()
            .ToList();

        // Build the pipeline chain (innermost = handler)
        RequestHandlerDelegate<object, object> pipeline = (req, cancellationToken) =>
        {
            var method = handlerType.GetMethod("Handle")
                ?? throw new InvalidOperationException("Handle method not found on handler");
            var result = method.Invoke(handler, new object[] { req, cancellationToken });
            return new ValueTask<object>(result!);
        };

        // Wrap behaviors around the handler (reverse order for correct execution)
        foreach (var behavior in behaviors.AsEnumerable().Reverse())
        {
            var next = pipeline;
            pipeline = (req, cancellationToken) =>
            {
                var method = behavior.GetType().GetMethod("Handle")
                    ?? throw new InvalidOperationException("Handle method not found on behavior");
                var result = method.Invoke(behavior, new object[] { req, cancellationToken, next });
                return new ValueTask<object>(result!);
            };
        }

        var finalResult = await pipeline(request, ct);
        return (TResponse)finalResult;
    }

    public async ValueTask Send(IRequest request, CancellationToken ct = default)
    {
        var requestType = request.GetType();
        var handlerType = typeof(IRequestHandler<>).MakeGenericType(requestType);
        var handler = _serviceProvider.GetRequiredService(handlerType);

        var method = handlerType.GetMethod("Handle")
            ?? throw new InvalidOperationException("Handle method not found on handler");

        var result = method.Invoke(handler, new object[] { request, ct });
        await (ValueTask)result!;
    }
}
