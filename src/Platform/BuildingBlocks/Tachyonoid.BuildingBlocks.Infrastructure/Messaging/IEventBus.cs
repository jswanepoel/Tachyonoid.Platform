namespace Tachyonoid.BuildingBlocks.Infrastructure;

/// <summary>
/// Event bus abstraction. Publishes integration events to RabbitMQ.
/// </summary>
public interface IEventBus
{
    Task PublishAsync<T>(T @event, CancellationToken ct = default) where T : class;
}
