namespace DomainDrivenDesignArchitucture.Infrastructure.Presistant.commons.primitives;

internal abstract class ApprovableEntity<T> : AuditableEntity<T>
{
    internal ApprovableEntity(T id, string creatorId, DateTime creationDate) : base(id, creatorId, creationDate) { }

    internal bool? IsApproved { get; set; }
}
