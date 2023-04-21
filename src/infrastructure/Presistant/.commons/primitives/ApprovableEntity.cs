namespace DomainDrivenDesignArchitucture.Infrastructure.Presistant.commons.primitives;

internal abstract class ApprovableEntity<T> : AuditableEntity<T>
{
    internal ApprovableEntity(T id) : base(id) { }

    internal bool? IsApproved { get; set; }
}
