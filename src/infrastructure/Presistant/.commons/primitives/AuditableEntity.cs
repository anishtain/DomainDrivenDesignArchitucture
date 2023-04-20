namespace DomainDrivenDesignArchitucture.Infrastructure.Presistant.commons.primitives;

internal abstract class AuditableEntity<T> : BaseEntity<T>
{
    internal AuditableEntity(T id, string creatorId, DateTime creationDate) : base(id, creatorId, creationDate) { }

    internal string UpdatorId { get; set; }

    internal DateTime? UpdateDate { get; set; }
}
