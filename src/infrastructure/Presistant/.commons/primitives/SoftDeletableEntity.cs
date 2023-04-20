namespace DomainDrivenDesignArchitucture.Infrastructure.Presistant.commons.primitives;

internal abstract class SoftDeletableEntity<T> : ApprovableEntity<T>
{
    internal SoftDeletableEntity(T id, string creatorId, DateTime creationDate) : base(id, creatorId, creationDate) { }

    internal bool IsDeleted { get; set; }

    internal string DeleterId { get; set; }

    internal DateTime? DeletionDate { get; set; }
}
