namespace DomainDrivenDesignArchitucture.Infrastructure.Presistant.commons.primitives;

internal abstract class BaseEntity<T>
{
    internal BaseEntity(T id)
    {
        Id = id;
    }

    internal T Id { get; init; }

    public string CreatorId { get;  set; }

    public DateTime CreateDate { get;  set; }

}
