using DomainDrivenDesignArchitucture.Domain.Models.commons.contracts;

namespace DomainDrivenDesignArchitucture.Domain.Models.commons.primitives;
public abstract class Aggregate<T> : Entity<T>
{
    private readonly List<IDomainEvent> _events = new();

    protected Aggregate(T id) : base(id) { }

    public void RaiseEvent(IDomainEvent @event) =>
        _events.Add(@event);
}
