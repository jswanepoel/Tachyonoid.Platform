namespace Tachyonoid.BuildingBlocks.Domain;

/// <summary>
/// Generic repository interface. One per aggregate root.
/// Implemented in the Persistence layer.
/// </summary>
public interface IRepository<T> where T : Entity
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default);
    Task AddAsync(T entity, CancellationToken ct = default);
    void Update(T entity);
    void Delete(T entity);
}

/// <summary>
/// Unit of Work pattern. Ensures all changes in a transaction commit together.
/// </summary>
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
