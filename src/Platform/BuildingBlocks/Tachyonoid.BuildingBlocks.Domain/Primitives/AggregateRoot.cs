namespace Tachyonoid.BuildingBlocks.Domain;

/// <summary>
/// Aggregate root base class. All aggregate roots derive from this.
/// Aggregate roots are the consistency boundary for transactions.
/// </summary>
public abstract class AggregateRoot<TId> : Entity<TId>, IHasDomainEvents
    where TId : notnull
{
    protected AggregateRoot() { }
    protected AggregateRoot(TId id) : base(id) { }

    // Entity<TId> already has DomainEvents, AddDomainEvent, RemoveDomainEvent, ClearDomainEvents
    // AggregateRoot exposes RaiseDomainEvent as the canonical method name
    protected void RaiseDomainEvent(DomainEvent domainEvent)
    {
        AddDomainEvent(domainEvent);
    }
}

/// <summary>
/// Aggregate root with Guid ID.
/// </summary>
public abstract class AggregateRoot : AggregateRoot<Guid>
{
    protected AggregateRoot() : base(Guid.NewGuid()) { }
    protected AggregateRoot(Guid id) : base(id) { }
}

/// <summary>
/// Interface for entities that have domain events.
/// </summary>
public interface IHasDomainEvents
{
    IReadOnlyCollection<DomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}
