namespace DomainDrivenDesignArchitucture.Domain.Models.commons.primitives;
public abstract class Entity<T> : IEquatable<Entity<T>>
{
    protected Entity(T id) => Id = id;

    public T Id { get; init; }

    public override bool Equals(object? obj) =>
        obj is not null && obj is Entity<T> entity && obj.GetType() == GetType() && Comparer<T>.Equals(entity.Id, Id);

    public override int GetHashCode() =>
        Id.GetHashCode();

    public bool Equals(Entity<T>? other) =>
        other is not null && other.GetType() == GetType() && Comparer<T>.Equals(other.Id, Id);

    public static bool operator == (Entity<T> first, Entity<T> second) =>
        first is not null && second is not null && first.GetType() == second.GetType() && Comparer<T>.Equals(first.Id, second.Id);

    public static bool operator !=(Entity<T> first, Entity<T> second) =>
        !(first == second);
}
