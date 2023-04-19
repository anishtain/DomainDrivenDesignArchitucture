namespace DomainDrivenDesignArchitucture.Domain.Models.commons.primitives;
public abstract class Aggregate<T> : Entity<T>
{
    protected Aggregate(T id) : base(id) { }
}
