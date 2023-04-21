namespace DomainDrivenDesignArchitucture.Infrastructure.Presistant.commons.primitives;

internal abstract class AuditableEntity<T> : BaseEntity<T>
{
    internal AuditableEntity(T id) : base(id) { }

    internal string UpdatorId { get; set; }

    internal DateTime? UpdateDate { get; set; }
}
