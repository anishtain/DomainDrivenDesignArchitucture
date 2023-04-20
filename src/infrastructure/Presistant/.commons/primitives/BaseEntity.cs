namespace DomainDrivenDesignArchitucture.Infrastructure.Presistant.commons.primitives;

internal abstract class BaseEntity<T>
{
    internal BaseEntity(T id, string creatorId, DateTime creationDate)
    {
        Id = id;
        CreatorId = creatorId;
        CreateDate = creationDate;
    }

    internal T Id { get; init; }

    public string CreatorId { get; init; }

    public DateTime CreateDate { get; init; }

}
