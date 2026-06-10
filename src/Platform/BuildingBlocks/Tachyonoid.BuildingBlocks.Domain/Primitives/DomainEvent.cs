namespace Tachyonoid.BuildingBlocks.Domain;

/// <summary>
/// Base domain event. All domain events derive from this.
/// Domain events are dispatched after the transaction commits (outbox pattern).
/// </summary>
public abstract record DomainEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
